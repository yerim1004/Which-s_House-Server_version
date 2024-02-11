Shader "Custom/CollectShader"
{
    Properties
    {
        _Color ("Color", Color) = (0.5,0,1,0.7) 
    }
    SubShader
    {
        Pass{
            Material{
                Diffuse[_Color]
            }
            Lighting On
        }
    }
    FallBack "Diffuse"
}
