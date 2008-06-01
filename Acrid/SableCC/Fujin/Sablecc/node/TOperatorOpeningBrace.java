/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import Dextr.Sablecc.analysis.*;

public final class TOperatorOpeningBrace extends Token
{
    public TOperatorOpeningBrace()
    {
        super.setText("{");
    }

    public TOperatorOpeningBrace(int line, int pos)
    {
        super.setText("{");
        setLine(line);
        setPos(pos);
    }

    public Object clone()
    {
      return new TOperatorOpeningBrace(getLine(), getPos());
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseTOperatorOpeningBrace(this);
    }

    public void setText(String text)
    {
        throw new RuntimeException("Cannot change TOperatorOpeningBrace text.");
    }
}