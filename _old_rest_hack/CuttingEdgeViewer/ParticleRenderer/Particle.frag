#version 330
uniform sampler2D tex;
in float brightness;
layout(location = 0) out vec4 color;
void main()
{
	vec4 texel = texture(tex, gl_PointCoord);
	texel *= brightness;
    color = vec4(texel.x, texel.y, texel.z, 1.0);
}