#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 texturePos;

out vec2 texPos;

uniform mat4 transform;

void main()
{
	gl_Position = vec4(aPosition, 1.0) * transform;
	texPos = texturePos;
}