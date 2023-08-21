using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    bool IsAggroed { get; set; }
    bool IsWithinStrikingDistance { get; set; }
    void SetAggroedStatus(bool isAggroed);
    void SetStrikingDistance(bool isWithinStrikingDistance);
}
