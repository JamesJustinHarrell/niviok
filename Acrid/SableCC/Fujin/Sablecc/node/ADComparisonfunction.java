/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import java.util.*;
import Dextr.Sablecc.analysis.*;

public final class ADComparisonfunction extends PComparisonfunction
{
    private TKeywordGte _keyword_gte_;

    public ADComparisonfunction ()
    {
    }

    public ADComparisonfunction (
            TKeywordGte _keyword_gte_
    )
    {
        setKeywordGte (_keyword_gte_);
    }

    public Object clone()
    {
        return new ADComparisonfunction (
            (TKeywordGte)cloneNode (_keyword_gte_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseADComparisonfunction(this);
    }

    public TKeywordGte getKeywordGte ()
    {
        return _keyword_gte_;
    }

    public void setKeywordGte (TKeywordGte node)
    {
        if(_keyword_gte_ != null)
        {
            _keyword_gte_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _keyword_gte_ = node;
    }

    public String toString()
    {
        return ""
            + toString (_keyword_gte_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _keyword_gte_ == child )
        {
            _keyword_gte_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _keyword_gte_ == oldChild )
        {
            setKeywordGte ((TKeywordGte) newChild);
            return;
        }
    }

}