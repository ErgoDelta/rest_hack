#version 330

uniform mat4 ViewMatrix;
uniform mat4 ModelMatrix;

in vec2 Position;
in vec2 TexCoord;

out	vec2 texCoord;

void main(void)
{
    vec4 position = ModelMatrix * vec4(Position, 0.0, 1.0);
    texCoord = TexCoord;
    gl_Position = ViewMatrix * position;
}

