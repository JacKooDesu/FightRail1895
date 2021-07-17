using UnityEngine;
using System.Collections;

namespace JacDev.Entity
{
    public class ProjectileObject : EntityObject
    {
        public Vector3 direction;
        public Transform hitObject = default;
        protected Vector3 hitPoint = Vector3.zero;

        public override bool GameUpdate()
        {
            return true;
        }

        public void Launch(Vector3 dir)
        {
            direction = dir;
            StartCoroutine(Flying());
        }

        public IEnumerator Flying()
        {
            float time = 0f;
            bool hasHit = false;
            Projectile setting = (Projectile)entitySetting;
            while (!hasHit && time <= setting.maxFlyTime)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, direction, out hit, setting.speed * Time.deltaTime, setting.collideLayer);

                if (hit.transform != null)
                {
                    hitObject = hit.transform;
                    hitPoint = hit.point;
                    hasHit = true;
                }

                transform.position += direction * setting.speed * Time.deltaTime;

                time += Time.deltaTime;

                yield return null;
            }

            if (hitObject != null)
                StartCoroutine(Explosive());
            else
                Destroy(gameObject);
        }

        IEnumerator Explosive()
        {
            Projectile setting = (Projectile)entitySetting;
            if (setting.radius == 0)
            {
                if (hitObject.GetComponent<EntityObject>() != null)
                    if (hitObject.transform.GetComponent<EntityObject>().entitySetting.entityType == setting.targetEntityType)
                        hitObject.transform.GetComponent<EntityObject>().GetDamage(setting.damage);

                Destroy(gameObject);
                yield break;
            }

            Transform explosive = Instantiate(setting.explosivePrefab, transform);
            explosive.position = hitObject.position;
            bool hasCalDamage = false;
            float t = 0;
            while (t <= 1f)
            {
                explosive.localScale = t * setting.radius * Vector3.one;
                if (!hasCalDamage && t >= .5f)
                {
                    RaycastHit[] hits = Physics.CapsuleCastAll(transform.position, transform.position, setting.radius, transform.forward, setting.collideLayer);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.GetComponent<EntityObject>() != null)
                        {
                            if (hit.transform.GetComponent<EntityObject>().entitySetting.entityType == setting.targetEntityType)
                            {
                                hit.transform.GetComponent<EntityObject>().GetDamage(setting.damage);
                                print(hit.transform.name);
                            }

                        }
                    }
                    hasCalDamage = true;
                }
                t += Time.deltaTime / setting.explosiveTime;

                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
