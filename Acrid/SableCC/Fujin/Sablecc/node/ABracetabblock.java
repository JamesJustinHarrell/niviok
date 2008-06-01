/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import java.util.*;
import Dextr.Sablecc.analysis.*;

public final class ABracetabblock extends PBracetabblock
{
    private TOperatorOpeningBrace _operator_opening_brace_;
    private TNewline _a_;
    private PTabblock _tabblock_;
    private TNewline _b_;
    private TOperatorClosingBrace _operator_closing_brace_;

    public ABracetabblock ()
    {
    }

    public ABracetabblock (
            TOperatorOpeningBrace _operator_opening_brace_,
            TNewline _a_,
            PTabblock _tabblock_,
            TNewline _b_,
            TOperatorClosingBrace _operator_closing_brace_
    )
    {
        setOperatorOpeningBrace (_operator_opening_brace_);
        setA (_a_);
        setTabblock (_tabblock_);
        setB (_b_);
        setOperatorClosingBrace (_operator_closing_brace_);
    }

    public Object clone()
    {
        return new ABracetabblock (
            (TOperatorOpeningBrace)cloneNode (_operator_opening_brace_),
            (TNewline)cloneNode (_a_),
            (PTabblock)cloneNode (_tabblock_),
            (TNewline)cloneNode (_b_),
            (TOperatorClosingBrace)cloneNode (_operator_closing_brace_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseABracetabblock(this);
    }

    public TOperatorOpeningBrace getOperatorOpeningBrace ()
    {
        return _operator_opening_brace_;
    }

    public void setOperatorOpeningBrace (TOperatorOpeningBrace node)
    {
        if(_operator_opening_brace_ != null)
        {
            _operator_opening_brace_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _operator_opening_brace_ = node;
    }
    public TNewline getA ()
    {
        return _a_;
    }

    public void setA (TNewline node)
    {
        if(_a_ != null)
        {
            _a_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _a_ = node;
    }
    public PTabblock getTabblock ()
    {
        return _tabblock_;
    }

    public void setTabblock (PTabblock node)
    {
        if(_tabblock_ != null)
        {
            _tabblock_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _tabblock_ = node;
    }
    public TNewline getB ()
    {
        return _b_;
    }

    public void setB (TNewline node)
    {
        if(_b_ != null)
        {
            _b_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _b_ = node;
    }
    public TOperatorClosingBrace getOperatorClosingBrace ()
    {
        return _operator_closing_brace_;
    }

    public void setOperatorClosingBrace (TOperatorClosingBrace node)
    {
        if(_operator_closing_brace_ != null)
        {
            _operator_closing_brace_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _operator_closing_brace_ = node;
    }

    public String toString()
    {
        return ""
            + toString (_operator_opening_brace_)
            + toString (_a_)
            + toString (_tabblock_)
            + toString (_b_)
            + toString (_operator_closing_brace_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _operator_opening_brace_ == child )
        {
            _operator_opening_brace_ = null;
            return;
        }
        if ( _a_ == child )
        {
            _a_ = null;
            return;
        }
        if ( _tabblock_ == child )
        {
            _tabblock_ = null;
            return;
        }
        if ( _b_ == child )
        {
            _b_ = null;
            return;
        }
        if ( _operator_closing_brace_ == child )
        {
            _operator_closing_brace_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _operator_opening_brace_ == oldChild )
        {
            setOperatorOpeningBrace ((TOperatorOpeningBrace) newChild);
            return;
        }
        if ( _a_ == oldChild )
        {
            setA ((TNewline) newChild);
            return;
        }
        if ( _tabblock_ == oldChild )
        {
            setTabblock ((PTabblock) newChild);
            return;
        }
        if ( _b_ == oldChild )
        {
            setB ((TNewline) newChild);
            return;
        }
        if ( _operator_closing_brace_ == oldChild )
        {
            setOperatorClosingBrace ((TOperatorClosingBrace) newChild);
            return;
        }
    }

}