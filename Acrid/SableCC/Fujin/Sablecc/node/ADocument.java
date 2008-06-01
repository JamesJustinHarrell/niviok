/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import java.util.*;
import Dextr.Sablecc.analysis.*;

public final class ADocument extends PDocument
{
    private TNewline _a_;
    private final LinkedList _import_ = new TypedLinkedList(new Import_Cast());
    private final LinkedList _scopealteration_ = new TypedLinkedList(new Scopealteration_Cast());
    private PDocumentinside _documentinside_;
    private TNewline _b_;

    public ADocument ()
    {
    }

    public ADocument (
            TNewline _a_,
            List _import_,
            List _scopealteration_,
            PDocumentinside _documentinside_,
            TNewline _b_
    )
    {
        setA (_a_);
        this._import_.clear();
        this._import_.addAll(_import_);
        this._scopealteration_.clear();
        this._scopealteration_.addAll(_scopealteration_);
        setDocumentinside (_documentinside_);
        setB (_b_);
    }

    public Object clone()
    {
        return new ADocument (
            (TNewline)cloneNode (_a_),
            cloneList (_import_),
            cloneList (_scopealteration_),
            (PDocumentinside)cloneNode (_documentinside_),
            (TNewline)cloneNode (_b_)
        );
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseADocument(this);
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
    public LinkedList getImport ()
    {
        return _import_;
    }

    public void setImport (List list)
    {
        _import_.clear();
        _import_.addAll(list);
    }
    public LinkedList getScopealteration ()
    {
        return _scopealteration_;
    }

    public void setScopealteration (List list)
    {
        _scopealteration_.clear();
        _scopealteration_.addAll(list);
    }
    public PDocumentinside getDocumentinside ()
    {
        return _documentinside_;
    }

    public void setDocumentinside (PDocumentinside node)
    {
        if(_documentinside_ != null)
        {
            _documentinside_.parent(null);
        }

        if(node != null)
        {
            if(node.parent() != null)
            {
                node.parent().removeChild(node);
            }

            node.parent(this);
        }

        _documentinside_ = node;
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

    public String toString()
    {
        return ""
            + toString (_a_)
            + toString (_import_)
            + toString (_scopealteration_)
            + toString (_documentinside_)
            + toString (_b_)
        ;
    }

    void removeChild(Node child)
    {
        if ( _a_ == child )
        {
            _a_ = null;
            return;
        }
        if ( _import_.remove(child))
        {
            return;
        }
        if ( _scopealteration_.remove(child))
        {
            return;
        }
        if ( _documentinside_ == child )
        {
            _documentinside_ = null;
            return;
        }
        if ( _b_ == child )
        {
            _b_ = null;
            return;
        }
    }

    void replaceChild(Node oldChild, Node newChild)
    {
        if ( _a_ == oldChild )
        {
            setA ((TNewline) newChild);
            return;
        }
        for(ListIterator i = _import_.listIterator(); i.hasNext();)
        {
            if(i.next() == oldChild)
            {
                if(newChild != null)
                {
                    i.set(newChild);
                    oldChild.parent(null);
                    return;
                }

                i.remove();
                oldChild.parent(null);
                return;
            }
        }
        for(ListIterator i = _scopealteration_.listIterator(); i.hasNext();)
        {
            if(i.next() == oldChild)
            {
                if(newChild != null)
                {
                    i.set(newChild);
                    oldChild.parent(null);
                    return;
                }

                i.remove();
                oldChild.parent(null);
                return;
            }
        }
        if ( _documentinside_ == oldChild )
        {
            setDocumentinside ((PDocumentinside) newChild);
            return;
        }
        if ( _b_ == oldChild )
        {
            setB ((TNewline) newChild);
            return;
        }
    }

    private class Import_Cast implements Cast
    {
        public Object cast(Object o)
        {
            PImport node = (PImport) o;

            if((node.parent() != null) &&
                (node.parent() != ADocument.this))
            {
                node.parent().removeChild(node);
            }

            if((node.parent() == null) ||
                (node.parent() != ADocument.this))
            {
                node.parent(ADocument.this);
            }

            return node;
        }
    }
    private class Scopealteration_Cast implements Cast
    {
        public Object cast(Object o)
        {
            PScopealteration node = (PScopealteration) o;

            if((node.parent() != null) &&
                (node.parent() != ADocument.this))
            {
                node.parent().removeChild(node);
            }

            if((node.parent() == null) ||
                (node.parent() != ADocument.this))
            {
                node.parent(ADocument.this);
            }

            return node;
        }
    }
}