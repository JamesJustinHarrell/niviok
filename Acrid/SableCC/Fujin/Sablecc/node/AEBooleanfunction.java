/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import java.util.*;
import Dextr.Sablecc.analysis.*;

public final class AEBooleanfunction extends PBooleanfunction
{
    private TKeywordXor _keyword_xor_;

    public AEBooleanfunction ()
    {
    }

    public AEBooleanfunction (
            TKeywordXor _keyword_xor_
    )
    {
        setKeywordXor (_keyword_xor_);
    }

    public Object clone()
    {
        return new AEBooleanfunction (
            (TKeywordXor)cloneNode (_keyword_xor_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseAEBooleanfunction(this);
    }

    public TKeywordXor getKeywordXor ()
    {
        return _keyword_xor_;
    }

    public void setKeywordXor (TKeywordXor node)
    {
        if(_keyword_xor_ != null)
        {
            _keyword_xor_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _keyword_xor_ = node;
    }

    public String toString()
    {
        return ""
            + toString (_keyword_xor_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _keyword_xor_ == child )
        {
            _keyword_xor_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _keyword_xor_ == oldChild )
        {
            setKeywordXor ((TKeywordXor) newChild);
            return;
        }
    }

}