///BSD 3 - Clause License

/// Copyright(c) 2021, ewanpandelus
///All rights reserved.

///Redistribution and use in source and binary forms, with or without
///modification, are permitted provided that the following conditions are met:

///1.Redistributions of source code must retain the above copyright notice, this
///list of conditions and the following disclaimer.

///2. Redistributions in binary form must reproduce the above copyright notice,
///this list of conditions and the following disclaimer in the documentation
///and/or other materials provided with the distribution.

///3. Neither the name of the copyright holder nor the names of its
///contributors may be used to endorse or promote products derived from
///this software without specific prior written permission.

///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
///AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
///IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
///FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
///DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
///SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
///CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
///OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    TravelingSalesman travellingSalesman;
    private Boolean selected = false;
    private Image image;
    private Color initialColour;
    Color lightGreen = new Color(131f / 255f, 243f / 255f, 127f / 255f, 1);

    void Start()
    {
        image = this.GetComponent<Image>();
        initialColour = image.color;
        travellingSalesman = TravelingSalesman.instance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selected)
        {
            SetAllColours(true);
            image.material.color = Color.white;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            SetAllColours(false);
            image.material.color = initialColour;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!travellingSalesman.GetSolved())
        {
            SetNode();
        }
    }

    /// <summary>
    /// Sets edges and distances corresponding to nodes played to a colour
    /// </summary>
    /// <param name="on">Whether to make the edge green or white</param>
    private void SetAllColours(bool on)
    {
        if (!(this.name == "A" && travellingSalesman.GetPlayedNodes().Count != GameObject.FindGameObjectsWithTag("Node").Length) && !travellingSalesman.GetSolved())
        {
            Distance distance = travellingSalesman.GetDistance(FindEdgeAssociatedWithNode());
            Edge edge = FindEdgeAssociatedWithNode();
            edge.setColour(on);
            image.material.color = Color.white;
            distance.setColour(on);
        }
    }

    /// <summary>
    /// Finds edges corresponding to last played node
    /// </summary>
    private Edge FindEdgeAssociatedWithNode()
    {
        try
        {
            if (GameObject.Find("(" + travellingSalesman.GetLastPlayedNode().name + "," + this.name + ")") == null)
            {
                return (GameObject.Find("(" + this.name + "," + travellingSalesman.GetLastPlayedNode().name + ")")).GetComponent<Edge>();
            }
            else
            {
                return GameObject.Find("(" + travellingSalesman.GetLastPlayedNode().name + "," + this.name + ")").GetComponent<Edge>();
            }
        }
        catch (NullReferenceException e)
        {
            UnityEngine.Debug.Log(e);
        }
        return null;
    }

    private void SwitchOnNode()
    {
        SetAllColours(true);
        selected = true;
    }

    /// <summary>
    /// If move is possible then node is added to list of nodes played 
    /// </summary>
    public void SetNode()
    {
        if (travellingSalesman.GetIsMovePossible(gameObject))
        {
            SwitchOnNode();
            travellingSalesman.SetPlayedNode(gameObject, false);
        }
    }

    public void SetWinningNode()
    {
        SwitchOnNode();
        travellingSalesman.SetPlayedNode(gameObject, true);
    }

    public void DeselectNode()
    {
        image.material.color = initialColour;
        selected = false;
    }
}
