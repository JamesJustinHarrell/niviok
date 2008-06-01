/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import java.util.*;
import Dextr.Sablecc.analysis.*;

public final class AADeclarefirstlist extends PDeclarefirstlist
{
    private PDeclarefirst _declarefirst_;

    public AADeclarefirstlist ()
    {
    }

    public AADeclarefirstlist (
            PDeclarefirst _declarefirst_
    )
    {
        setDeclarefirst (_declarefirst_);
    }

    public Object clone()
    {
        return new AADeclarefirstlist (
            (PDeclarefirst)cloneNode (_declarefirst_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseAADeclarefirstlist(this);
    }

    public PDeclarefirst getDeclarefirst ()
    {
        return _declarefirst_;
    }

    public void setDeclarefirst (PDeclarefirst node)
    {
        if(_declarefirst_ != null)
        {
            _declarefirst_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _declarefirst_ = node;
    }

    public String toString()
    {
        return ""
            + toString (_declarefirst_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _declarefirst_ == child )
        {
            _declarefirst_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _declarefirst_ == oldChild )
        {
            setDeclarefirst ((PDeclarefirst) newChild);
            return;
        }
    }

}