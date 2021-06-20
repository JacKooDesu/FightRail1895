using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class KinoAnalogGlitchPass : ScriptableRenderPass
{

    string profilerTag;
    Material material;
    RenderTargetIdentifier cameraColorTargetIdent;
    RenderTargetHandle tempTexture;

    #region Public Properties

    // Scan line jitter

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

    #endregion

    #region Private Properties

    [SerializeField] Shader _shader;

    float _verticalJumpTime;

    #endregion

    public KinoAnalogGlitchPass(
        string profilerTag,
        RenderPassEvent renderPassEvent,
        Shader shader)
    {
        this.profilerTag = profilerTag;
        this._shader = shader;
        this.renderPassEvent = renderPassEvent;
    }

    public void Setup(RenderTargetIdentifier cameraColorTargetIdent)
    {
        this.cameraColorTargetIdent = cameraColorTargetIdent;
    }

    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (material == null)
        {
            material = new Material(_shader);
            material.hideFlags = HideFlags.DontSave;
        }

        _verticalJumpTime += Time.deltaTime * _verticalJump * 11.3f;

        var sl_thresh = Mathf.Clamp01(1.0f - _scanLineJitter * 1.2f);
        var sl_disp = 0.002f + Mathf.Pow(_scanLineJitter, 3) * 0.05f;
        material.SetVector("_ScanLineJitter", new Vector2(sl_disp, sl_thresh));

        var vj = new Vector2(_verticalJump, _verticalJumpTime);
        material.SetVector("_VerticalJump", vj);

        material.SetFloat("_HorizontalShake", _horizontalShake * 0.2f);

        var cd = new Vector2(_colorDrift * 0.04f, Time.time * 606.11f);
        material.SetVector("_ColorDrift", cd);

        CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
        cmd.Clear();

        cmd.Blit(cameraColorTargetIdent, tempTexture.Identifier(), material, 0);

        cmd.Blit(tempTexture.Identifier(), cameraColorTargetIdent);

        context.ExecuteCommandBuffer(cmd);

        cmd.Clear();
        CommandBufferPool.Release(cmd);
    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(tempTexture.id);
    }
}
