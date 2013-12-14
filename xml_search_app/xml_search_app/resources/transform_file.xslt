<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:template match="address_book">
    <html>
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"></meta>
          <title>Address Book search results</title>
          <style>
            html body {
            background: none repeat scroll 0 0 #F4F4F4;
            }

            body, button, input, label, select, td, textarea {
            font-family: 'lucida grande',tahoma,verdana,arial,sans-serif;
            font-size: 11px;
            }

            body {
            background: none repeat scroll 0 0 #FFFFFF;
            color: #333333;
            direction: ltr;
            line-height: 1.28;
            margin: 0;
            padding: 0;
            text-align: left;
            unicode-bidi: embed;
            }
            h1 {
            font-family: 'Freight Sans Bold','lucida grande',tahoma,verdana,arial,sans-serif;
            font-weight: normal;
            text-rendering: optimizelegibility;
            }

            .content {
            background: none repeat scroll 0 0 #FFFFFF;
            border: 1px solid #D9D9D9;
            border-radius: 3px;
            box-shadow: 0 1px 1px #DDDDDD;
            margin: auto;
            max-width: 940px;
            padding: 25px 20px 20px;
            overflow: hidden;
            }

            .content .header {
            margin: auto;
            max-width: 940px;
            text-align: center;
            }
            .addressBookContainer {

            }
            .addressBookContainer .column {
            border: 1px solid;
            border-color: #D2DEE5;
            float: left;
            width: 33%;
            }
            .addressBookContainer .label {
            background: none repeat scroll 0 0 #F2F7FC;
            border-color: #D2DEE5 !important;
            font-size: 13px;
            font-weight: bold;
            text-align: center;
            }
            .addressBookContainer .cell {
            border-bottom-width: 1px;
            border-bottom-style: solid;
            border-color: inherit;
            font-family: verdana,tahoma,arial,sans-serif;
            padding: 10px 0;
            text-align: center;
            }
          </style>
        </head>
      <body>
        <div class="content">
          <div class="header">
            <h1>Address Book search results</h1>
          </div>
          <div class="addressBookContainer">
            <div class="column">
              <div class="cell label">Full name</div>
                <xsl:apply-templates select="item/full_name"/>
            </div>
            <div class="column">
              <div class="cell label">Address</div>
              <xsl:apply-templates select="item/address"/>
            </div>
            <div class="column">
              <div class="cell label">Phone number</div>
              <xsl:apply-templates select="item/phone_number"/>
            </div>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
  
  <xsl:template match="item/full_name">
    <div class="cell">
      <xsl:value-of select="."/>
    </div>
  </xsl:template>

  <xsl:template match="item/address">
    <div class="cell">
      <xsl:value-of select="."/>
    </div>
  </xsl:template>

  <xsl:template match="item/phone_number">
    <div class="cell">
      <xsl:value-of select="."/>
    </div>
  </xsl:template>
  
</xsl:stylesheet>

