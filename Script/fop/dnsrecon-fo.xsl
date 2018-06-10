<?xml version="1.0" encoding="UTF-8"?>
<!-- =========================================================================
		dnsrecon-fo.xls stylesheet version 0.01
		Part of WebMVA - Collaborative Application for
		Vulnerability Assessment Analysis
		https://github.com/unicam-webmva/webmva
		last change: 2018-06-09
		Riccardo Cannella
		University of Camerino - Italy

		Usage
		==============

		* Run dnsrecon with xml flag for xml output

		* Convert output xml to pdf using the above xsl file with fop:
		       $ fop -xml dnsrecon.scan.xml -xsl dnsrecon-fo.xsl -pdf dnsrecon.scan.pdf

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
					<xsl:apply-templates select="records"/>
				</fo:flow>
			</fo:page-sequence>
		</fo:root>
	</xsl:template>
<!-- ............................................................ -->

	<!-- records -->
	<xsl:template match="records">
		<fo:block font-size="18pt" font-family="sans-serif" font-weight="bold" background-color="#2A0D45" color="#FFFFFF" margin-bottom="5pt" id="head" text-align="center">
			DnsRecon Scan Report
		</fo:block>
		<fo:block font-size="10pt" font-family="sans-serif" margin-bottom="5pt">
			Scanned at <xsl:value-of select="scaninfo/@time"/> - Domain: <xsl:value-of select="domain/@domain_name"/>
		</fo:block>
		<fo:block margin-top="5pt"/>
		<xsl:apply-templates select="record[@type = 'info']"/>
		<xsl:call-template name="testaNoZoneTransfer"/>
		<fo:block font-size='9pt' font-family="sans-serif" padding-top="3pt">
			<xsl:apply-templates select="record[@type='SOA'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='NS'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='TXT'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='PTR'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='MX'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='AAAA'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='A'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='CNAME'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='SRV'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='HINFO'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='RP'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='ASFDB'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='LOC'][not(@zone_server)]"/>
			<xsl:apply-templates select="record[@type='NAPTR'][not(@zone_server)]"/>
		</fo:block>
		<fo:block margin-top="5pt"/>
		<xsl:apply-templates select="scaninfo"/>
	</xsl:template>
	<!-- record tipo info -->
	<xsl:template match="record[@type = 'info']">
		<!-- applicare prima success poi failed -->
		<xsl:apply-templates select="record[@zone_tranfer = 'success']"/>
		<xsl:apply-templates select="record[@zone_tranfer = 'failed']"/>
	</xsl:template>
	<!-- record tipo zone_tranfer="success" -->
	<xsl:template match="record[@zone_transfer = 'success']">
		<fo:block font-size="14pt" font-family="sans-serif" background-color="#2A0D45" color="#FFFFFF" padding-top="3pt">
			<xsl:text>Server con zone transfer eseguito con successo: </xsl:text><xsl:value-of select="@ns_server"/>
		</fo:block>
		<xsl:variable name="var_address"><xsl:value-of select="@ns_server"/></xsl:variable>
		<fo:block font-size="12pt" font-family="sans-serif" background-color="#2A0D45" color="#FFFFFF" padding-top="3pt" margin-bottom="5pt">
			<xsl:text>Elenco dei server trovati con zone transfer: </xsl:text>
		</fo:block>
		<fo:block/>
		<fo:block/>
		<fo:block font-size='9pt' font-family="sans-serif" padding-top="3pt" margin-left="0.5cm">
			<xsl:for-each select="../record">
				<xsl:variable name="zonesrv"><xsl:value-of select="@zone_server"/></xsl:variable>
				<xsl:if test="$var_address = $zonesrv">
					<!-- applicare ogni tipo di template -->
					<xsl:if test="@type='SOA'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			MNAME:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@mname"/><xsl:text> | </xsl:text>
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='NS'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Target:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@target"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
								Version:
							</fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@Version"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
								Recursive:
							</fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@recursive"/><xsl:text> | </xsl:text>
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='TXT'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Strings:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@strings"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='PTR'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Name:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
                    <xsl:if test="@type='MX'">
					    <fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Exchange:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@exchange"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Name:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
                    <xsl:if test="@type='AAAA'">
					    <fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Name:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
                    <xsl:if test="@type='A'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Name:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
                    <xsl:if test="@type='CNAME'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Name:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Target:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@target"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='SRV'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Indirizzo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Name:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Target:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@target"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Port:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@port"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Weight:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@weight"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='HINFO'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			CPU:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@cpu"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			OS:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@os"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='RP'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			mbox:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@mbox"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Txt:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@txt"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='ASFDB'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Hostname:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@hostname"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Subtype:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@subtype"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='LOC'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Coordinate:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@coordinates"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
					<xsl:if test="@type='NAPTR'">
						<fo:block font-size="7pt">
							<xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Tipo:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
		                    <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Order:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@order"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Preference:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@preference"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			RegEx:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@regex"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Replacement:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@replacement"/><xsl:text> | </xsl:text>
							<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
                    			Service:
			                </fo:inline>
							<xsl:text> </xsl:text><xsl:value-of select="@service"/><xsl:text> | </xsl:text>
		                    
			            </fo:block>
						<fo:block><fo:leader leader-pattern="rule" leader-length="17cm" /></fo:block>
					</xsl:if>
				</xsl:if>
			</xsl:for-each>
		</fo:block>
		<fo:block page-break-after="always"/>
	</xsl:template>
	<!-- record tipo zone_tranfer="failed" -->
	<xsl:template match="record[@zone_transfer='failed']">
		<fo:block font-size="14pt" font-family="sans-serif" background-color="#2A0D45" color="#FFFFFF" padding-top="3pt">
			<xsl:text>Server con zone transfer eseguito senza successo: </xsl:text><xsl:value-of select="@ns_server"/>
		</fo:block>
	</xsl:template>
	<!-- titolo per tutti gli altri risultati -->
	<xsl:template name="testaNoZoneTransfer">
		<fo:block font-size="14pt" font-family="sans-serif" background-color="#2A0D45" color="#FFFFFF" padding-top="3pt" margin-bottom="5pt">
			<xsl:text>Risultati scan:</xsl:text>
		</fo:block>
	</xsl:template>
	<!-- record tipo SOA" -->
	<xsl:template match="record[@type='SOA'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				MNAME:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@mname"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo NS" -->
	<xsl:template match="record[@type='NS'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Target:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@target"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Version:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@Version"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Recursive:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@recursive"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo TXT" -->
	<xsl:template match="record[@type='TXT'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Strings:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@strings"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo PTR" -->
	<xsl:template match="record[@type='PTR'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Name:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo MX" -->
	<xsl:template match="record[@type='MX'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Exchange:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@exchange"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Name:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo AAAA" -->
	<xsl:template match="record[@type='AAAA'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Name:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo A" -->
	<xsl:template match="record[@type='A'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Name:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo CNAME" -->
	<xsl:template match="record[@type='CNAME'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Name:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Target:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@target"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo SRV" -->
	<xsl:template match="record[@type='SRV'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Indirizzo:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@address"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Name:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@name"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Target:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@target"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Port:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@port"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Weight:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@weight"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo HINFO" -->
	<xsl:template match="record[@type='HINFO'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			CPU:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@cpu"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				OS:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@os"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo RP" -->
	<xsl:template match="record[@type='HINFO'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			MBox:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@mbox"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Txt:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@txt"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo ASFDB" -->
	<xsl:template match="record[@type='ASFDB'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Hostname:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@hostname"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Subtype:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@subtype"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo LOC" -->
	<xsl:template match="record[@type='LOC'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Coordinate:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@coordinates"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>
	<!-- record tipo NAPTR" -->
	<xsl:template match="record[@type='NAPTR'][not(@zone_server)]">
		<fo:block font-size="7pt">
			<xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="4pt" text-align="left" background-color="#CCFFCC" color="#006400">
            	Tipo:
            </fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@type"/><xsl:text> | </xsl:text>
            <fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			Order:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@order"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Preference:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@preference"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
	   			RegEx:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@regex"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Replacement:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@replacement"/><xsl:text> | </xsl:text>
			<fo:inline font-size="8pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" padding-bottom="3pt" text-align="left" background-color="#CCFFCC" color="#006400">
				Service:
			</fo:inline>
			<xsl:text> </xsl:text><xsl:value-of select="@service"/><xsl:text> | </xsl:text>
		</fo:block>
		<fo:block><fo:leader leader-pattern="rule" leader-length="18cm" /></fo:block>
	</xsl:template>

	<!-- scaninfo -->
	<xsl:template match="scaninfo">	
		<fo:block font-size="11pt" font-family="sans-serif" font-weight="bold" padding-top="3pt" text-align="left" background-color="#F0F8FF" color="#000000" id="summary">
			Scan Summary
		</fo:block>
	
		<fo:block font-size="8pt" font-family="sans-serif" padding-top="6pt" color="#000000">
			Dnsrecon scan was initiated at <xsl:value-of select="/records/scaninfo/@time" /> with these arguments:
		</fo:block>
	
		<fo:block font-size="8pt" font-family="sans-serif" font-style="italic" background-color="#CCCCCC" color="#000000">
			<xsl:value-of select="/records/scaninfo/@arguments" />
		</fo:block>

		<fo:block>&#160;</fo:block>

		<fo:block font-size="6pt" font-family="sans-serif" padding-top="10pt">
			<fo:basic-link internal-destination="head">back to top</fo:basic-link>
		</fo:block>

		<fo:block>&#160;</fo:block>

	</xsl:template>
<!-- ............................................................ -->

</xsl:stylesheet>


