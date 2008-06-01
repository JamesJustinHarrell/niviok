/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import Dextr.Sablecc.analysis.*;

public final class TKeywordDne extends Token
{
    public TKeywordDne()
    {
        super.setText("dne");
    }

    public TKeywordDne(int line, int pos)
    {
        super.setText("dne");
        setLine(line);
        setPos(pos);
    }

    public Object clone()
    {
      return new TKeywordDne(getLine(), getPos());
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseTKeywordDne(this);
    }

    public void setText(String text)
    {
        throw new RuntimeException("Cannot change TKeywordDne text.");
    }
}