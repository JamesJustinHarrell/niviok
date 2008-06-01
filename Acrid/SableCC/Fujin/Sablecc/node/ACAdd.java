/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import java.util.*;
import Dextr.Sablecc.analysis.*;

public final class ACAdd extends PAdd
{
    private PAdd _add_;
    private TOperatorMinus _operator_minus_;
    private PMult _mult_;

    public ACAdd ()
    {
    }

    public ACAdd (
            PAdd _add_,
            TOperatorMinus _operator_minus_,
            PMult _mult_
    )
    {
        setAdd (_add_);
        setOperatorMinus (_operator_minus_);
        setMult (_mult_);
    }

    public Object clone()
    {
        return new ACAdd (
            (PAdd)cloneNode (_add_),
            (TOperatorMinus)cloneNode (_operator_minus_),
            (PMult)cloneNode (_mult_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseACAdd(this);
    }

    public PAdd getAdd ()
    {
        return _add_;
    }

    public void setAdd (PAdd node)
    {
        if(_add_ != null)
        {
            _add_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _add_ = node;
    }
    public TOperatorMinus getOperatorMinus ()
    {
        return _operator_minus_;
    }

    public void setOperatorMinus (TOperatorMinus node)
    {
        if(_operator_minus_ != null)
        {
            _operator_minus_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _operator_minus_ = node;
    }
    public PMult getMult ()
    {
        return _mult_;
    }

    public void setMult (PMult node)
    {
        if(_mult_ != null)
        {
            _mult_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _mult_ = node;
    }

    public String toString()
    {
        return ""
            + toString (_add_)
            + toString (_operator_minus_)
            + toString (_mult_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _add_ == child )
        {
            _add_ = null;
            return;
        }
        if ( _operator_minus_ == child )
        {
            _operator_minus_ = null;
            return;
        }
        if ( _mult_ == child )
        {
            _mult_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _add_ == oldChild )
        {
            setAdd ((PAdd) newChild);
            return;
        }
        if ( _operator_minus_ == oldChild )
        {
            setOperatorMinus ((TOperatorMinus) newChild);
            return;
        }
        if ( _mult_ == oldChild )
        {
            setMult ((PMult) newChild);
            return;
        }
    }

}