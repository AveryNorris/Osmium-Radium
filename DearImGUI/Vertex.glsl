#version 330 core

layout (location = 0) in vec2 pos;
layout (location = 1) in vec2 uv;
layout (location = 2) in vec4 col;

uniform mat4 proj;


out vec2 fragUV;
out vec4 fragCol;

void main()
{
    fragUV = uv;
    fragCol = col;
    gl_Position = proj * vec4(pos.x, pos.y, 0, 1);
}