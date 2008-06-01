/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import Dextr.Sablecc.analysis.*;

public final class Start extends Node
{
    private PDocument _base_;
    private EOF _eof_;

    public Start()
    {
    }

    public Start(
        PDocument _base_,
        EOF _eof_)
    {
        setPDocument(_base_);
        setEOF(_eof_);
    }

    public Object clone()
    {
        return new Start(
            (PDocument) cloneNode(_base_),
            (EOF) cloneNode(_eof_));
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseStart(this);
    }

    public PDocument getPDocument()
    {
        return _base_;
    }
    public void setPDocument(PDocument node)
    {
        if(_base_ != null)
        {
            _base_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _base_ = node;
    }

    public EOF getEOF()
    {
        return _eof_;
    }
    public void setEOF(EOF node)
    {
        if(_eof_ != null)
        {
            _eof_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _eof_ = node;
    }

    void removeChild(Node child)
    {
        if(_base_ == child)
        {
            _base_ = null;
            return;
        }

        if(_eof_ == child)
        {
            _eof_ = null;
            return;
        }
    }
    void replaceChild(Node oldChild, Node newChild)
    {
        if(_base_ == oldChild)
        {
            setPDocument((PDocument) newChild);
            return;
        }

        if(_eof_ == oldChild)
        {
            setEOF((EOF) newChild);
            return;
        }
    }

    public String toString()
    {
        return "" +
            toString(_base_) +
            toString(_eof_);
    }
}