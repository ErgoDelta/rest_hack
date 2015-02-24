#version 330

uniform sampler2D TextureSampler;
uniform vec4 Color = vec4(1,1,1,1);

in vec2 texCoord;

out vec4 fragColor;

void main(void)
{
	fragColor = Color * texture(TextureSampler, texCoord);
}