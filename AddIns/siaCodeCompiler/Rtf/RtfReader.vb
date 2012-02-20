'*******************************************************************************
' *   This file is part of NRtfTree Library.
' *
' *   NRtfTree Library is free software; you can redistribute it and/or modify
' *   it under the terms of the GNU Lesser General Public License as published by
' *   the Free Software Foundation; either version 3 of the License, or
' *   (at your option) any later version.
' *
' *   NRtfTree Library is distributed in the hope that it will be useful,
' *   but WITHOUT ANY WARRANTY; without even the implied warranty of
' *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' *   GNU Lesser General Public License for more details.
' *
' *   You should have received a copy of the GNU Lesser General Public License
' *   along with this program. If not, see <http://www.gnu.org/licenses/>.
' *******************************************************************************


'*******************************************************************************
' * Library:		NRtfTree
' * Version:     v0.3.0
' * Date:		02/09/2007
' * Copyright:   2007 Salvador Gomez
' * E-mail:      sgoliver.net@gmail.com
' * Home Page:	http://www.sgoliver.net
' * SF Project:	http://nrtftree.sourceforge.net
' *				http://sourceforge.net/projects/nrtftree
' * Class:		RtfReader
' * Description:	Analizador secuencial de documentos RTF.
' * *****************************************************************************


Imports System.IO

Namespace Net.Sgoliver.NRtfTree
	Namespace Core
		''' <summary>
		''' Esta clase proporciona los m�todos necesarios para la carga y an�lisis secuencial de un documento RTF.
		''' </summary>
		Public Class RtfReader
			#Region "Atributos privados"

			Private rtf As TextReader
			'Fichero/Cadena de entrada RTF
			Private lex As RtfLex
			'Analizador l�xico para RTF
			Private tok As RtfToken
			'Token actual
			Private reader As SarParser
			'Rtf Reader
			#End Region

			#Region "Constructores"

			''' <summary>
			''' Constructor de la clase RtfReader.
			''' </summary>
			''' <param name="reader">
			''' Objeto del tipo SARParser que contienen los m�todos necesarios para el tratamiento de los
			''' distintos elementos de un documento RTF.
			''' </param>
			Public Sub New(reader As SarParser)
				' Inicializados por defecto 

				'lex = null;
				'tok = null;
				'rtf = null;

				Me.reader = reader
			End Sub

			#End Region

			#Region "M�todos P�blicos"

			''' <summary>
			''' Carga un documento RTF dada la ruta del fichero que lo contiene.
			''' </summary>
			''' <param name="path">Ruta del fichero que contiene el documento RTF.</param>
			''' <returns>
			''' Resultado de la carga del documento. Si la carga se realiza correctamente
			''' se devuelve el valor 0.
			''' </returns>
			Public Function LoadRtfFile(path As String) As Integer
				'Resultado de la carga
				Dim res As Integer = 0

				'Se abre el fichero de entrada
				rtf = New StreamReader(path)

				'Se crea el analizador l�xico para RTF
				lex = New RtfLex(rtf)

				'Se devuelve el resultado de la carga
				Return res
			End Function

			''' <summary>
			''' Carga un documento RTF dada la cadena de caracteres que lo contiene.
			''' </summary>
			''' <param name="text">Cadena de caractres que contiene el documento RTF.</param>
			''' <returns>
			''' Resultado de la carga del documento. Si la carga se realiza correctamente
			''' se devuelve el valor 0.
			''' </returns>
			Public Function LoadRtfText(text As String) As Integer
				'Resultado de la carga
				Dim res As Integer = 0

				'Se abre el fichero de entrada
				rtf = New StringReader(text)

				'Se crea el analizador l�xico para RTF
				lex = New RtfLex(rtf)

				'Se devuelve el resultado de la carga
				Return res
			End Function

			''' <summary>
			''' Comienza el an�lisis del documento RTF y provoca la llamada a los distintos m�todos 
			''' del objeto IRtfReader indicado en el constructor de la clase.
			''' </summary>
			''' <returns>
			''' Resultado del an�lisis del documento. Si la carga se realiza correctamente
			''' se devuelve el valor 0.
			''' </returns>
			Public Function Parse() As Integer
				'Resultado del an�lisis
				Dim res As Integer = 0

				'Comienza el documento
				reader.StartRtfDocument()

				'Se obtiene el primer token
				tok = lex.NextToken()

				While tok.Type <> RtfTokenType.Eof
					Select Case tok.Type
						Case RtfTokenType.GroupStart
							reader.StartRtfGroup()
							Exit Select
						Case RtfTokenType.GroupEnd
							reader.EndRtfGroup()
							Exit Select
						Case RtfTokenType.Keyword
							reader.RtfKeyword(tok.Key, tok.HasParameter, tok.Parameter)
							Exit Select
						Case RtfTokenType.Control
							reader.RtfControl(tok.Key, tok.HasParameter, tok.Parameter)
							Exit Select
						Case RtfTokenType.Text
							reader.RtfText(tok.Key)
							Exit Select
						Case Else
							res = -1
							Exit Select
					End Select

					'Se obtiene el siguiente token
					tok = lex.NextToken()
				End While

				'Finaliza el documento
				reader.EndRtfDocument()

				'Se cierra el stream
				rtf.Close()

				Return res
			End Function

			#End Region
		End Class
	End Namespace
End Namespace
