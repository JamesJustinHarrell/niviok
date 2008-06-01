/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import Dextr.Sablecc.analysis.*;

public final class TKeywordEql extends Token
{
    public TKeywordEql()
    {
        super.setText("eql");
    }

    public TKeywordEql(int line, int pos)
    {
        super.setText("eql");
        setLine(line);
        setPos(pos);
    }

    public Object clone()
    {
      return new TKeywordEql(getLine(), getPos());
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseTKeywordEql(this);
    }

    public void setText(String text)
    {
        throw new RuntimeException("Cannot change TKeywordEql text.");
    }
}