using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fungus;

public class FungusCustomListner : MonoBehaviour {
    [SerializeField] UnityEvent<Writer> OnInput;
    [SerializeField] UnityEvent<Writer, WriterState> OnStart;
    [SerializeField] UnityEvent<Writer, WriterState> OnPause;
    [SerializeField] UnityEvent<Writer, WriterState> OnResume;
    [SerializeField] UnityEvent<Writer, WriterState> OnEnd;
    [SerializeField] UnityEvent<Writer> OnGlyph;

    void Start() {
        WriterSignals.OnWriterState += _CallStateFunction;
        WriterSignals.OnWriterInput += _CallInputFunction;
        WriterSignals.OnWriterGlyph += _CallGlyphFunction;
    }

    void OnDestroy() {
        WriterSignals.OnWriterState -= _CallStateFunction;
        WriterSignals.OnWriterInput -= _CallInputFunction;
        WriterSignals.OnWriterGlyph -= _CallGlyphFunction;
    }

    void _CallInputFunction(Writer writer) {
        try {
            OnInput?.Invoke(writer);
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    void _CallGlyphFunction(Writer writer) {
        try {
            OnGlyph?.Invoke(writer);
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    void _CallStateFunction(Writer writer, WriterState state) {
        try {
            switch (state) {
                case WriterState.Start:
                    OnStart?.Invoke(writer, state);
                    break;
                case WriterState.Pause:
                    OnPause?.Invoke(writer, state);
                    break;
                case WriterState.Resume:
                    OnResume?.Invoke(writer, state);
                    break;
                case WriterState.End:
                    OnEnd?.Invoke(writer, state);
                    break;
            }
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }
}
