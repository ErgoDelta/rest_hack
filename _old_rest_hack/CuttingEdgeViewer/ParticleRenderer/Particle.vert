#version 330
layout(location = 0) in vec4 position;
out float brightness;
void main()
{
    gl_Position = vec4(position.xyz, 1.0);
	brightness = position.w;
}
