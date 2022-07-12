using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BlackScreenEffect : MonoBehaviour
{
    public Material effectMaterial;
    /*
    public float displacement_magnitude = 0.025f;
    public float mask_magnitude = 0.5f;
    public readonly string _magnitude_name = "_Magnitude";
    public readonly string _Maskmagnitude_name = "_MaskTex_mag";*/

    public float maskRangeSize = 1;
    public readonly string _RangeSize = "_RangeSize";

    public Texture2D overLapImg;
    public readonly string _OverLapImage = "_OverLapImage";
    /*  
    ??**************!ONLY WORK FOR SRP!*****************

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, effectMaterial);
    }
    ??***********************************************/

    /*
    ??**************!ONLY WORK FOR URP! *****************??*************************************************/
    RenderTexture render_Tex;
    public Camera renderProviderCamera;
    public Camera mainCamera;
    public int facter = 4;
     void OnEnable()
    {
        mainCamera = Camera.main;
        render_Tex = new RenderTexture(mainCamera.pixelWidth >> facter, mainCamera.pixelHeight >> facter, 16);
        render_Tex = new RenderTexture(mainCamera.pixelWidth,
                                        mainCamera.pixelHeight, 16);
        renderProviderCamera.targetTexture = render_Tex;
        effectMaterial.mainTexture = render_Tex;

        //Shader.SetGlobalFloat(_magnitude_name, displacement_magnitude);
        Shader.SetGlobalFloat(_RangeSize, maskRangeSize);
        RenderPipelineManager.endCameraRendering += EndCameraRendering;
    }

    private void OnDestroy()
    {
        RenderPipelineManager.endCameraRendering -= EndCameraRendering;
    }

    public void SetMaskMag(float newMag) {
        //mask_magnitude = newMag;
        //Shader.SetGlobalFloat(_Maskmagnitude_name, mask_magnitude);
        maskRangeSize = newMag;
        Shader.SetGlobalFloat(_RangeSize, maskRangeSize);
    }


    void EndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera == mainCamera)
        {
            Graphics.Blit(render_Tex, mainCamera.targetTexture, effectMaterial);
        }
    }
    

    private void OnValidate()
    {
        //only in editor
        /*
        Shader.SetGlobalFloat(_magnitude_name, displacement_magnitude);
        Shader.SetGlobalFloat(_Maskmagnitude_name, mask_magnitude);*/
        Shader.SetGlobalFloat(_RangeSize, maskRangeSize);
    }




}
