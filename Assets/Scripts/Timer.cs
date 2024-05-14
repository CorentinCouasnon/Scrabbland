using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Timer
{
    readonly MonoBehaviour _coroutineRunner;
    Coroutine _coroutine;

    float _total;

    float _nextTick;
    bool _running;

    public bool TickAtStart { get; set; }
    public float Every { get; set; }

    public float Total
    {
        get => _total;
        set
        {
            _total = value;
            Remaining = value;
        }
    }

    public float Jitter { get; set; }
    public Action Ticked { get; set; }
    public Action Elapsed { get; set; }

    public float Remaining { get; private set; }

    public Timer(MonoBehaviour coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void Start()
    {
        if (Total == 0)
            throw new InvalidOperationException("Can't start timer that has a starting time of 0.");

        if (Jitter > Every)
        {
            Debug.LogWarning($"The value of {nameof(Jitter)} cannot exceed the value of {nameof(Every)} because it may cause unintended" +
                             $"behavior. {nameof(Jitter)} has been set to the value of {nameof(Every)}.");
            Jitter = Every;
        }

        Remaining = Total;

        if (Every == 0 && Jitter == 0)
        {
            _nextTick = -1;
        }
        else
        {
            _nextTick = Total - Every + Random.Range(-Jitter, Jitter);
        }

        _running = true;
        _coroutine = _coroutineRunner.StartCoroutine(Run());

        if (TickAtStart)
            Ticked?.Invoke();
    }

    public void Pause()
    {
        _running = false;
    }

    public void Resume()
    {
        _running = true;
    }

    public void Stop()
    {
        if (_coroutineRunner != null && _coroutine != null)
            _coroutineRunner.StopCoroutine(_coroutine);

        _running = false;
    }

    IEnumerator Run()
    {
        while (Remaining > 0)
        {
            Elapse();
            yield return null;
        }

        Elapsed?.Invoke();
        _running = false;
    }

    void Elapse()
    {
        if (!_running)
            return;

        Remaining -= Time.deltaTime;

        if (Remaining > _nextTick)
            return;

        Ticked?.Invoke();
        _nextTick -= Every + Random.Range(-Jitter, Jitter);
    }
}