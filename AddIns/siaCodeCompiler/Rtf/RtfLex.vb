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
' * Class:		RtfLex
' * Description:	Analizador l�xico de documentos RTF.
' * *****************************************************************************


Imports System.IO
Imports System.Text

Namespace Net.Sgoliver.NRtfTree
	Namespace Core
		''' <summary>
		''' Analizador l�xico (tokenizador) para documentos en formato RTF. Analiza el documento y devuelve de 
		''' forma secuencial todos los elementos RTF leidos (tokens).
		''' </summary>
		Public Class RtfLex
			#Region "Atributos privados"

			''' <summary>
			''' Fichero abierto.
			''' </summary>
			Private rtf As TextReader

			#End Region

			#Region "Constantes"

			''' <summary>
			''' Marca de fin de fichero.
			''' </summary>
      Private Const Eof As Integer = -1

			#End Region

			#Region "Constructores"

			''' <summary>
			''' Constructor de la clase RtfLex
			''' </summary>
			''' <param name="rtfReader">Stream del fichero a analizar.</param>
			Public Sub New(rtfReader As TextReader)
				rtf = rtfReader
			End Sub

			#End Region

			#Region "M�todos P�blicos"

			''' <summary>
			''' Lee un nuevo token del documento RTF.
			''' </summary>
			''' <returns>Siguiente token leido del documento.</returns>
			Public Function NextToken() As RtfToken
				'Caracter leido del documento
        Dim c As Integer, ch As Char

				'Se crea el nuevo token a devolver
				Dim token As New RtfToken()

				'Se lee el siguiente caracter del documento
        c = rtf.Read() : ch = ChrW(c)

				'Se ignoran los retornos de carro, tabuladores y caracteres nulos
        While ch = ControlChars.Cr OrElse ch = ControlChars.Lf OrElse ch = ControlChars.Tab OrElse ch = ControlChars.NullChar
          c = rtf.Read() : ch = ChrW(c)
        End While

				'Se trata el caracter leido
				If c <> Eof Then
          Select Case ch
            Case "{"c
              token.Type = RtfTokenType.GroupStart
              Exit Select
            Case "}"c
              token.Type = RtfTokenType.GroupEnd
              Exit Select
            Case "\"c
              parseKeyword(token)
              Exit Select
            Case Else
              token.Type = RtfTokenType.Text
              parseText(c, token)
              Exit Select
          End Select
				Else
					'Fin de fichero
					token.Type = RtfTokenType.Eof
				End If

				Return token
			End Function


			#End Region

			#Region "M�todos Privados"

			''' <summary>
			''' Lee una palabra clave del documento RTF.
			''' </summary>
			''' <param name="token">Token RTF al que se asignar� la palabra clave.</param>
			Private Sub parseKeyword(token As RtfToken)
				Dim palabraClave As New StringBuilder()

				Dim parametroStr As New StringBuilder()
				Dim parametroInt As Integer = 0

				Dim c As Integer
				Dim negativo As Boolean = False

				c = rtf.Peek()

				'Si el caracter leido no es una letra --> Se trata de un s�mbolo de Control o un caracter especial: '\\', '\{' o '\}'
				If Not [Char].IsLetter(ChrW(c)) Then
					rtf.Read()

          If c = Asc("\"c) OrElse c = Asc("{"c) OrElse c = Asc("}"c) Then
            'Caracter especial
            token.Type = RtfTokenType.Text
            token.Key = (ChrW(c)).ToString()
          Else
            'Simbolo de control
            token.Type = RtfTokenType.Control
            token.Key = (ChrW(c)).ToString()

            'Si se trata de un caracter especial (codigo de 8 bits) se lee el par�metro hexadecimal
            If token.Key = "'" Then
              Dim cod As String = ""

              cod += ChrW(rtf.Read())
              cod += ChrW(rtf.Read())

              token.HasParameter = True

              token.Parameter = Convert.ToInt32(cod, 16)

              'TODO: �Hay m�s s�mbolos de Control con par�metros?
            End If
          End If

					Return
				End If

				'Se lee la palabra clave completa (hasta encontrar un caracter no alfanum�rico, por ejemplo '\' � ' '
				c = rtf.Peek()
				While [Char].IsLetter(ChrW(c))
					rtf.Read()
					palabraClave.Append(ChrW(c))

					c = rtf.Peek()
				End While

				'Se asigna la palabra clave leida
				token.Type = RtfTokenType.Keyword
				token.Key = palabraClave.ToString()

				'Se comprueba si la palabra clave tiene par�metro
        If [Char].IsDigit(ChrW(c)) OrElse c = Asc("-"c) Then
          token.HasParameter = True

          'Se comprubea si el par�metro es negativo
          If c = Asc("-"c) Then
            negativo = True

            rtf.Read()
          End If

          'Se lee el par�metro completo
          c = rtf.Peek()
          While [Char].IsDigit(ChrW(c))
            rtf.Read()
            parametroStr.Append(ChrW(c))

            c = rtf.Peek()
          End While

          parametroInt = Convert.ToInt32(parametroStr.ToString())

          If negativo Then
            parametroInt = -parametroInt
          End If

          'Se asigna el par�metro de la palabra clave
          token.Parameter = parametroInt
        End If

        If c = Asc(" "c) Then
          rtf.Read()
        End If
			End Sub

			''' <summary>
			''' Lee una cadena de Texto del documento RTF.
			''' </summary>
			''' <param name="car">Primer caracter de la cadena.</param>
			''' <param name="token">Token RTF al que se asignar� la palabra clave.</param>
			Private Sub parseText(car As Integer, token As RtfToken)
				Dim c As Integer = car

				Dim Texto As New StringBuilder((ChrW(c)).ToString(), 3000000)

				c = rtf.Peek()

				'Se ignoran los retornos de carro, tabuladores y caracteres nulos
        While c = Asc(ControlChars.Cr) OrElse c = Asc(ControlChars.Lf) OrElse c = Asc(ControlChars.Tab) OrElse c = Asc(ControlChars.NullChar)
          rtf.Read()
          c = rtf.Peek()
        End While

        While Asc(c <> Asc("\"c)) AndAlso Asc(c <> Asc("}"c)) AndAlso Asc(c <> Asc("{"c)) AndAlso c <> Eof
          rtf.Read()

          Texto.Append(ChrW(c))

          c = rtf.Peek()

          'Se ignoran los retornos de carro, tabuladores y caracteres nulos
          While c = Asc(ControlChars.Cr) OrElse c = Asc(ControlChars.Lf) OrElse c = Asc(ControlChars.Tab) OrElse c = Asc(ControlChars.NullChar)
            rtf.Read()
            c = rtf.Peek()
          End While
        End While

				token.Key = Texto.ToString()
			End Sub

			#End Region
		End Class
	End Namespace
End Namespace
