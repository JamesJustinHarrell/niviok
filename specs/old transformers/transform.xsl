<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	
	<xsl:import href="/home/justin/docbook5-xsl-1.72.0/html/docbook.xsl"/>
	
	<!-- create lists of specified things for book elements, and nothing else -->
	<xsl:param name="generate.toc">
		book toc,title,figure,table,example,equation
	</xsl:param>
	
	<!-- add numbers to sections -->
	<xsl:param name="section.autolabel" select="1"></xsl:param>
	
	<xsl:param name="section.label.includes.component.label" select="1"></xsl:param>
	
	<!-- add <link> to stylesheet -->
	<xsl:param name="html.stylesheet">styling.css</xsl:param>
	
	<!-- prevent the boolean and leaf nodes from being hidden -->
	<xsl:param name="toc.max.depth">8</xsl:param>
	<xsl:param name="toc.section.depth">8</xsl:param>
	
</xsl:stylesheet>