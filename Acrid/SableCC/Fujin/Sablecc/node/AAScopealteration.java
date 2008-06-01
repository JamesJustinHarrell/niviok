/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import java.util.*;
import Dextr.Sablecc.analysis.*;

public final class AAScopealteration extends PScopealteration
{
    private PExpose _expose_;

    public AAScopealteration ()
    {
    }

    public AAScopealteration (
            PExpose _expose_
    )
    {
        setExpose (_expose_);
    }

    public Object clone()
    {
        return new AAScopealteration (
            (PExpose)cloneNode (_expose_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseAAScopealteration(this);
    }

    public PExpose getExpose ()
    {
        return _expose_;
    }

    public void setExpose (PExpose node)
    {
        if(_expose_ != null)
        {
            _expose_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _expose_ = node;
    }

    public String toString()
    {
        return ""
            + toString (_expose_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _expose_ == child )
        {
            _expose_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _expose_ == oldChild )
        {
            setExpose ((PExpose) newChild);
            return;
        }
    }

}