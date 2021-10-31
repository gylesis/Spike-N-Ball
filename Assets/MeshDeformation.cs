using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MeshDeformation : MonoBehaviour
{
    private Mesh Mesh;
    private Vector3[] Vertices;
    private Tween[] Deformation;
    [SerializeField] private MeshFilter MeshFilter;
    [SerializeField] private AnimationCurve Curve;
    [SerializeField] private Ease Ease;

    private void Start()
    {
        Mesh = MeshFilter.mesh;
        Deformation = new Tween[Mesh.vertexCount];
        Vertices = Mesh.vertices;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space)) Deform(Vector2.down, 0.5f, 0.5f);
    }

    public void Deform(Vector2 direction, float strength, float duration)
    {
        for (int i = 0; i < Mesh.vertexCount; i++)
        {
            const float diameter = 0.64f;
            var hitPosition = direction.normalized * diameter / 2;
            float deformStrength;

            if (Vertices[i] == Vector3.zero)
                deformStrength = 1;
            else
                deformStrength = Vector2.Distance(hitPosition, Vertices[i]) / diameter;

            var deformDirection = hitPosition - (Vector2) Vertices[i];
            var transition = (Vector3) deformDirection.normalized * Curve.Evaluate(deformStrength) * strength / 2;
            var vertexDestination = Vertices[i] + transition;

            if (Deformation[i] != null && Deformation[i].active)
            {
                Deformation[i].Complete();
                Deformation[i].Kill();
            }

            int index = i;
            Deformation[i] = DOTween.To(() => Vertices[index], x => Vertices[index] = x,
                vertexDestination, duration).SetLoops(2, LoopType.Yoyo);
            Deformation[i].OnUpdate(UpdateMesh);
            Deformation[i].SetEase(Ease);
        }
    }

    void UpdateMesh()
    {
        Mesh.vertices = Vertices;
        Mesh.RecalculateBounds();
    }

    private void OnDrawGizmos()
    {
        if (Vertices == null) return;
        foreach (var vertex in Vertices)
        {
            Gizmos.DrawSphere(transform.position + vertex, vertex == Vector3.zero ? 0.1f : 0.02f);
        }
    }
}