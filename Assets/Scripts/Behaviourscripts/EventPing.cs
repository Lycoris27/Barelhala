using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/EventPing")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "EventPing", message: "SendEventPing", category: "Events", id: "ed33ab546182525a2e99374c2bb5248c")]
public sealed partial class EventPing : EventChannel { }

