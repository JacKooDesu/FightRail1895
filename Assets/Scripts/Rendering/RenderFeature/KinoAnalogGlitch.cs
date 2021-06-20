using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JacDev.Rendering
{
    public class KinoAnalogGlitch : ScriptableRendererFeature
    {
        [System.Serializable]
        public class GlitchSettings
        {
            public bool IsEnabled = true;
            public RenderPassEvent WhenToInsert = RenderPassEvent.AfterRendering;

            [SerializeField, Range(0, 1)]
            float _scanLineJitter = 0;

            public float scanLineJitter
            {
                get { return _scanLineJitter; }
                set { _scanLineJitter = value; }
            }

            // Vertical jump

            [SerializeField, Range(0, 1)]
            float _verticalJump = 0;

            public float verticalJump
            {
                get { return _verticalJump; }
                set { _verticalJump = value; }
            }

            // Horizontal shake

            [SerializeField, Range(0, 1)]
            float _horizontalShake = 0;

            public float horizontalShake
            {
                get { return _horizontalShake; }
                set { _horizontalShake = value; }
            }

            // Color drift

            [SerializeField, Range(0, 1)]
            float _colorDrift = 0;

            public float colorDrift
            {
                get { return _colorDrift; }
                set { _colorDrift = value; }
            }

            public Shader shader;

            public Material material;

            float _verticalJumpTime;
        }

        public GlitchSettings settings = new GlitchSettings();

        RenderTargetHandle renderTextureHandle;
        KinoAnalogGlitchPass glitchPass;

        public override void Create()
        {
            glitchPass = new KinoAnalogGlitchPass(
                "Analog Glitch",
                settings.WhenToInsert,
                settings.shader
            );
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (settings.IsEnabled)
            {
                return;
            }

            var cameraColorTargetIdent = renderer.cameraColorTarget;
            glitchPass.Setup(cameraColorTargetIdent);

            renderer.EnqueuePass(glitchPass);
        }

    }
}

