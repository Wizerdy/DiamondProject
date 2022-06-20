using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizzInformation : MonoBehaviour
{
    [SerializeField] private int entryId;
    [SerializeField] private TextMeshProUGUI informationTxtComponent;
    [SerializeField] private PosterityObject posterity;
    [SerializeField] private hintType hintType;
    [SerializeField] private GameObject btnResponse;
    private string informationText;
    private bool knowText = false;
    private void Start() {
        switch (hintType) {
            case hintType.FallBoss:
                for (int i = 0; i < posterity.fallBossHintList.Count; i++) {
                    if (posterity.fallBossHintList[i].id == entryId) {
                        if (posterity.fallBossHintList[i].isTrigger) {
                            informationText = posterity.fallBossHintList[i].unlockedText;
                            btnResponse.SetActive(true);
                            knowText = true;
                        }
                        else
                            informationText = posterity.fallBossHintList[i].defaultText;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.FallAttack:
                for (int i = 0; i < posterity.fallAttackHintList.Count; i++) {
                    if (posterity.fallAttackHintList[i].id == entryId) {
                        if (posterity.fallAttackHintList[i].isTrigger) {
                            informationText = posterity.fallAttackHintList[i].unlockedText;
                            btnResponse.SetActive(true);
                            knowText = true;
                        } else
                            informationText = posterity.fallAttackHintList[i].defaultText;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.WinterBoss:
                for (int i = 0; i < posterity.winterBossHintList.Count; i++) {
                    if (posterity.winterBossHintList[i].id == entryId) {
                        if (posterity.winterBossHintList[i].isTrigger) {
                            informationText = posterity.winterBossHintList[i].unlockedText;
                            btnResponse.SetActive(true);
                            knowText = true;
                        } else
                            informationText = posterity.winterBossHintList[i].defaultText;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.WinterAttack:
                for (int i = 0; i < posterity.winterAttackHintList.Count; i++) {
                    if (posterity.winterAttackHintList[i].id == entryId) {
                        if (posterity.winterAttackHintList[i].isTrigger) {
                            informationText = posterity.winterAttackHintList[i].unlockedText;
                            btnResponse.SetActive(true);
                            knowText = true;
                        } else
                            informationText = posterity.winterAttackHintList[i].defaultText;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.FallName:
                for (int i = 0; i < posterity.fallBossHintList.Count; i++) {
                    if (posterity.fallBossHintList[i].id == entryId) {
                        if (posterity.fallBossHintList[i].isTrigger) {
                            informationText = posterity.fallBossHintList[i].unlockedName;
                            knowText = true;
                        }
                        else
                            informationText = posterity.fallBossHintList[i].defaultName;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.FallAttackName:
                for (int i = 0; i < posterity.fallAttackHintList.Count; i++) {
                    if (posterity.fallAttackHintList[i].id == entryId) {
                        if (posterity.fallAttackHintList[i].isTrigger) {
                            informationText = posterity.fallAttackHintList[i].unlockedName;
                            knowText = true;
                        }
                        else
                            informationText = posterity.fallAttackHintList[i].defaultName;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.WinterName:
                for (int i = 0; i < posterity.winterBossHintList.Count; i++) {
                    if (posterity.winterBossHintList[i].id == entryId) {
                        if (posterity.winterBossHintList[i].isTrigger) {
                            informationText = posterity.winterBossHintList[i].unlockedName;
                            knowText = true;
                        }
                        else
                            informationText = posterity.winterBossHintList[i].defaultName;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            case hintType.WinterAttackName:
                for (int i = 0; i < posterity.winterAttackHintList.Count; i++) {
                    if (posterity.winterAttackHintList[i].id == entryId) {
                        if (posterity.winterAttackHintList[i].isTrigger) {
                            informationText = posterity.winterAttackHintList[i].unlockedName;
                            knowText = true;
                        }
                        else
                            informationText = posterity.winterAttackHintList[i].defaultName;

                        informationTxtComponent.text = informationText;
                        return;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void OnEnable() {
        if (!knowText) {
            switch (hintType) {
                case hintType.FallBoss:
                    for (int i = 0; i < posterity.fallBossHintList.Count; i++) {
                        if (posterity.fallBossHintList[i].id == entryId) {
                            if (posterity.fallBossHintList[i].isTrigger) {
                                informationText = posterity.fallBossHintList[i].unlockedText;
                                btnResponse.SetActive(true);
                                knowText = true;
                            } else
                                informationText = posterity.fallBossHintList[i].defaultText;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                case hintType.FallAttack:
                    for (int i = 0; i < posterity.fallAttackHintList.Count; i++) {
                        if (posterity.fallAttackHintList[i].id == entryId) {
                            if (posterity.fallAttackHintList[i].isTrigger) {
                                informationText = posterity.fallAttackHintList[i].unlockedText;
                                btnResponse.SetActive(true);
                                knowText = true;
                            } else
                                informationText = posterity.fallAttackHintList[i].defaultText;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                case hintType.WinterBoss:
                    for (int i = 0; i < posterity.winterBossHintList.Count; i++) {
                        if (posterity.winterBossHintList[i].id == entryId) {
                            if (posterity.winterBossHintList[i].isTrigger) {
                                informationText = posterity.winterBossHintList[i].unlockedText;
                                btnResponse.SetActive(true);
                                knowText = true;
                            } else
                                informationText = posterity.winterBossHintList[i].defaultText;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                case hintType.WinterAttack:
                    for (int i = 0; i < posterity.winterAttackHintList.Count; i++) {
                        if (posterity.winterAttackHintList[i].id == entryId) {
                            if (posterity.winterAttackHintList[i].isTrigger) {
                                informationText = posterity.winterAttackHintList[i].unlockedText;
                                btnResponse.SetActive(true);
                                knowText = true;
                            } else
                                informationText = posterity.winterAttackHintList[i].defaultText;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                case hintType.FallName:
                    for (int i = 0; i < posterity.fallBossHintList.Count; i++) {
                        if (posterity.fallBossHintList[i].id == entryId) {
                            if (posterity.fallBossHintList[i].isTrigger) {
                                informationText = posterity.fallBossHintList[i].unlockedName;
                                knowText = true;
                            } else
                                informationText = posterity.fallBossHintList[i].defaultName;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                case hintType.FallAttackName:
                    for (int i = 0; i < posterity.fallAttackHintList.Count; i++) {
                        if (posterity.fallAttackHintList[i].id == entryId) {
                            if (posterity.fallAttackHintList[i].isTrigger) {
                                informationText = posterity.fallAttackHintList[i].unlockedName;
                                knowText = true;
                            } else
                                informationText = posterity.fallAttackHintList[i].defaultName;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                case hintType.WinterName:
                    for (int i = 0; i < posterity.winterBossHintList.Count; i++) {
                        if (posterity.winterBossHintList[i].id == entryId) {
                            if (posterity.winterBossHintList[i].isTrigger) {
                                informationText = posterity.winterBossHintList[i].unlockedName;
                                knowText = true;
                            } else
                                informationText = posterity.winterBossHintList[i].defaultName;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                case hintType.WinterAttackName:
                    for (int i = 0; i < posterity.winterAttackHintList.Count; i++) {
                        if (posterity.winterAttackHintList[i].id == entryId) {
                            if (posterity.winterAttackHintList[i].isTrigger) {
                                informationText = posterity.winterAttackHintList[i].unlockedName;
                                knowText = true;
                            } else
                                informationText = posterity.winterAttackHintList[i].defaultName;

                            informationTxtComponent.text = informationText;
                            return;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
