<?xml version="1.0" encoding="UTF-8"?>
<!-- =========================================================================
		dnsenum-fo.xls stylesheet version 0.01
		Part of WebMVA - Collaborative Application for
		Vulnerability Assessment Analysis
		https://github.com/unicam-webmva/webmva
		last change: 2018-06-09
		Riccardo Cannella
		University of Camerino - Italy

		Usage
		==============

		* Run dnsenum with -o flag for xml output

		* Convert output xml to pdf using the above xsl file with fop:
		       $ fop -xml dnsenum.scan.xml -xsl dnsenum-fo.xsl -pdf dnsenum.scan.pdf

========================================================================== -->

<xsl:stylesheet version="1.0" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:fo="http://www.w3.org/1999/XSL/Format">

<!-- Base Document -->
	<xsl:template match="/">
		<fo:root>
			<fo:layout-master-set>
				<fo:simple-page-master master-name="simple" page-height="29.7cm" page-width="21cm" margin-bottom="1cm" margin-left="1.5cm" margin-right="1.5cm">
					<fo:region-body margin-top="1cm"/>
					<fo:region-before extent="3cm"/>
					<fo:region-after extent="1.5cm"/>
				</fo:simple-page-master>
			</fo:layout-master-set>

			<fo:page-sequence master-reference="simple">
				<fo:flow flow-name="xsl-region-body">
					<xsl:apply-templates select="magictree/testdata/webmva"/>
					<xsl:apply-templates select="magictree/testdata"/>
				</fo:flow>
			</fo:page-sequence>
		</fo:root>
	</xsl:template>
<!-- ............................................................ -->
<!-- Intestazione aggiunta per report WEBMVA -->
<xsl:template match="magictree/testdata/webmva">
	<fo:block font-size="18pt" font-family="sans-serif" font-weight="bold" background-color="#2A0D45" color="#FFFFFF" margin-bottom="1cm" text-align="center">
			WebMVA - DNSEnum Scan Report
	</fo:block>
	<fo:block margin-top="5pt">
	Progetto: <xsl:value-of select="scaninfo/progetto"/>
	</fo:block>
	<fo:block margin-top="5pt">
	Nome modulo: <xsl:value-of select="scaninfo/modulo"/>
	</fo:block>
	<fo:block margin-top="5pt">
	Comando: <xsl:value-of select="scaninfo/comando"/>
	</fo:block>
	<fo:block margin-top="5pt">
	Target: <xsl:value-of select="scaninfo/target"/>
	</fo:block>
	<fo:block margin-top="5pt">
	Data di esecuzione: <xsl:value-of select="scaninfo/data"/>
	</fo:block>
	<fo:block margin-bottom="5pt"/>
</xsl:template>



	<!-- magictree/testdata -->
	<xsl:template match="magictree/testdata">
		<fo:block font-size="18pt" font-family="sans-serif" font-weight="bold" background-color="#2A0D45" color="#FFFFFF" margin-bottom="1cm" id="head" text-align="center">
			DNSEnum Scan Report
		</fo:block>
		<fo:block margin-top="5pt"/>
		<xsl:apply-templates select="host"/>
		<fo:block font-size="14pt" font-family="sans-serif" background-color="#125632" color="#FFFFFF" padding-top="3pt" margin-top="1cm">
			<xsl:text>FQDN trovati: </xsl:text>
		</fo:block>
		<xsl:apply-templates select="fqdn"/>
		</xsl:template>
	<!-- host -->
	<xsl:template match="host">
		<fo:block font-size="14pt" font-family="sans-serif" background-color="#125632" color="#FFFFFF" padding-top="3pt">
			<xsl:text>Server: </xsl:text><xsl:value-of select="."/>
		</fo:block>
		<fo:block font-size='9pt' font-family="sans-serif" padding-top="3pt" margin-left="0.5cm">
			<xsl:text>Hostname: </xsl:text><xsl:value-of select="./hostname"/>
		</fo:block>
	</xsl:template>
	<!-- fqdn -->
	<xsl:template match="fqdn">
		<fo:block font-size='9pt' font-family="sans-serif" padding-top="3pt" margin-left="0.5cm">
			<xsl:value-of select="."/>
		</fo:block>
	</xsl:template>
<!-- ............................................................ -->

</xsl:stylesheet>


