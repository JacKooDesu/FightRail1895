using UnityEngine;
using System.Collections;

namespace JacDev.Entity
{
    public class ProjectileObject : EntityObject
    {
        protected Projectile setting;
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
            while (hitObject == null && time <= setting.maxFlyTime)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, direction, out hit, setting.speed);

                if (hit.transform != null)
                {
                    hitObject = hit.transform;
                    hitPoint = hit.point;
                }

                yield return null;
            }

            if (hitObject != null)
                Explosive();
        }

        IEnumerator Explosive()
        {
            if (setting.radius == 0)
            {
                if (hitObject.GetComponent<EntityObject>() != null)
                    if (hitObject.transform.GetComponent<EntityObject>().entitySetting.entityType == setting.targetEntityType)
                        hitObject.transform.GetComponent<EntityObject>().GetDamage(setting.damage);

                yield break;
            }

            Transform explosive = Instantiate(setting.explosivePrefab);
            explosive.position = hitObject.position;
            bool hasCalDamage = false;
            float t = 0;
            while (t <= 1)
            {
                explosive.localScale = t * setting.radius * Vector3.one;
                if (!hasCalDamage && t >= .5f)
                {
                    RaycastHit[] hits = Physics.CapsuleCastAll(transform.position, transform.position, setting.radius, transform.forward);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.GetComponent<EntityObject>() != null)
                        {
                            if (hit.transform.GetComponent<EntityObject>().entitySetting.entityType == setting.targetEntityType)
                                hit.transform.GetComponent<EntityObject>().GetDamage(setting.damage);
                        }
                    }
                }
                t += Time.deltaTime;

                yield return null;
            }
        }
    }
}
