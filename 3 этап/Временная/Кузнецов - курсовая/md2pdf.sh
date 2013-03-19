#!/bin/bash
#===============================================================================
#
#          FILE:  md2pdf.sh
# 
#         USAGE:  ./md2pdf.sh <markdown file>
# 
#   DESCRIPTION:  simple Markdown to pdf render script
# 
#       OPTIONS:  ---
#  REQUIREMENTS:  python-markdown, xhtml2pdf
#          BUGS:  ---
#         NOTES:  ---
#        AUTHOR:  Yuri Kuznetsov , teddypickerfromul@gmail.com
#       COMPANY:  
#       VERSION:  1.0
#       CREATED:  28.02.2013 21:25:33 MSK
#      REVISION:  ---
#===============================================================================

DEFAULT_CSS_FILE=style.css

MARKDOWN_FILE=$1.mkd
HTML_FILE=$1.html
PDF_FILE=$1.pdf

touch $PDF_FILE
markdown_py $MARKDOWN_FILE -f $HTML_FILE
xhtml2pdf --quiet --css $DEFAULT_CSS_FILE $HTML_FILE $PDF_FILE
rm $HTML_FILE
