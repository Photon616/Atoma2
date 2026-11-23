using UnityEngine;

public class AudioGod : MonoBehaviour
{
    public static AudioGod Instance{ get; private set; }

    [SerializeField] private AudioClip snip;
    [SerializeField] private AudioClip snap;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip boop;
    [SerializeField] private AudioClip tick;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMute(bool doMute)
    {
        audioSource.enabled = doMute;
    }

    public void Snip()
    {
        audioSource.PlayOneShot(snip);
    }

    public void Snap()
    {
        audioSource.PlayOneShot(snap);
    }

    public void Pop()
    {
        audioSource.PlayOneShot(pop);
    }

    public void Boop()
    {
        audioSource.PlayOneShot(boop);
    }

    public void Tick()
    {
        audioSource.PlayOneShot(tick);
    }
}
