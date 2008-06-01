/* This file was generated by SableCC (http://www.sablecc.org/). */

package Dextr.Sablecc.node;

import Dextr.Sablecc.analysis.*;

public final class TKeywordImport extends Token
{
    public TKeywordImport()
    {
        super.setText("import");
    }

    public TKeywordImport(int line, int pos)
    {
        super.setText("import");
        setLine(line);
        setPos(pos);
    }

    public Object clone()
    {
      return new TKeywordImport(getLine(), getPos());
    }

    public void apply(Switch sw)
    {
        ((Analysis) sw).caseTKeywordImport(this);
    }

    public void setText(String text)
    {
        throw new RuntimeException("Cannot change TKeywordImport text.");
    }
}