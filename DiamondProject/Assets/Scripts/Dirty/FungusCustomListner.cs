using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fungus;

public class FungusCustomListner : MonoBehaviour, IWriterListener {
    enum State {
        INPUT, START, PAUSE, RESUME, END, GLYPH, ALL_WORDS_WRITTEN, VOICE_OVER
    }

    [SerializeField] UnityEvent _onInput;
    [SerializeField] UnityEvent _onStart;
    [SerializeField] UnityEvent _onPause;
    [SerializeField] UnityEvent _onResume;
    [SerializeField] UnityEvent _onEnd;
    [SerializeField] UnityEvent _onGlyph;
    [SerializeField] UnityEvent _onAllWordsWritten;
    [SerializeField] UnityEvent _onVoiceOver;

    void _CallStateFunction(State state) {
        try {
            switch (state) {
                case State.INPUT:
                    _onInput?.Invoke();
                    break;
                case State.START:
                    _onStart?.Invoke();
                    break;
                case State.PAUSE:
                    _onPause?.Invoke();
                    break;
                case State.RESUME:
                    _onResume?.Invoke();
                    break;
                case State.END:
                    _onEnd?.Invoke();
                    break;
                case State.GLYPH:
                    _onGlyph?.Invoke();
                    break;
                case State.ALL_WORDS_WRITTEN:
                    _onAllWordsWritten?.Invoke();
                    break;
                case State.VOICE_OVER:
                    _onVoiceOver?.Invoke();
                    break;
                default:
                    break;
            }
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    void IWriterListener.OnInput() {
        _CallStateFunction(State.INPUT);
    }

    void IWriterListener.OnStart(AudioClip audioClip) {
        _CallStateFunction(State.START);
    }

    void IWriterListener.OnPause() {
        _CallStateFunction(State.PAUSE);
    }

    void IWriterListener.OnResume() {
        _CallStateFunction(State.RESUME);
    }

    void IWriterListener.OnEnd(bool stopAudio) {
        _CallStateFunction(State.END);
    }

    void IWriterListener.OnGlyph() {
        _CallStateFunction(State.GLYPH);
    }

    public void OnAllWordsWritten() {
        _CallStateFunction(State.ALL_WORDS_WRITTEN);
    }

    public void OnVoiceover(AudioClip voiceOverClip) {
        _CallStateFunction(State.VOICE_OVER);
    }
}
