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
' * Class:		InfoGroup
' * Description:	Clase para encapsular toda la informaci�n contenida en un
' *              grupo RTF de tipo "\info".
' * *****************************************************************************


Imports System.Text

Namespace Net.Sgoliver.NRtfTree
	Namespace Util
		''' <summary>
		''' Clase que encapsula toda la informaci�n contenida en un grupo "\info" de un documento RTF.
		''' </summary>
		Public Class InfoGroup
			#Region "Atributos privados"

			Private _title As String = ""
			Private _subject As String = ""
			Private _author As String = ""
			Private _manager As String = ""
			Private _company As String = ""
			Private _operator As String = ""
			Private _category As String = ""
			Private _keywords As String = ""
			Private _comment As String = ""
			Private _doccomm As String = ""
			Private _hlinkbase As String = ""
			Private _creatim As DateTime = DateTime.MinValue
			Private _revtim As DateTime = DateTime.MinValue
			Private _printim As DateTime = DateTime.MinValue
			Private _buptim As DateTime = DateTime.MinValue
			Private _version As Integer = -1
			Private _vern As Integer = -1
			Private _edmins As Integer = -1
			Private _nofpages As Integer = -1
			Private _nofwords As Integer = -1
			Private _nofchars As Integer = -1
			Private _id As Integer = -1

			#End Region

			#Region "Propiedades"

			''' <summary>
			''' T�tulo del documento.
			''' </summary>
			Public Property Title() As String
				Get
					Return _title
				End Get
				Set
					_title = value
				End Set
			End Property

			''' <summary>
			''' Tema del documento.
			''' </summary>
			Public Property Subject() As String
				Get
					Return _subject
				End Get
				Set
					_subject = value
				End Set
			End Property

			''' <summary>
			''' Autor del documento.
			''' </summary>
			Public Property Author() As String
				Get
					Return _author
				End Get
				Set
					_author = value
				End Set
			End Property

			''' <summary>
			''' Manager del autor del documento.
			''' </summary>
			Public Property Manager() As String
				Get
					Return _manager
				End Get
				Set
					_manager = value
				End Set
			End Property

			''' <summary>
			''' Compa��a del autor del documento.
			''' </summary>
			Public Property Company() As String
				Get
					Return _company
				End Get
				Set
					_company = value
				End Set
			End Property

			''' <summary>
			''' �ltima persona que ha realizao cambios sobre el documento.
			''' </summary>
			Public Property [Operator]() As String
				Get
					Return _operator
				End Get
				Set
					_operator = value
				End Set
			End Property

			''' <summary>
			''' Categor�a del documento.
			''' </summary>
			Public Property Category() As String
				Get
					Return _category
				End Get
				Set
					_category = value
				End Set
			End Property

			''' <summary>
			''' Palabras clave del documento.
			''' </summary>
			Public Property Keywords() As String
				Get
					Return _keywords
				End Get
				Set
					_keywords = value
				End Set
			End Property

			''' <summary>
			''' Comentarios.
			''' </summary>
			Public Property Comment() As String
				Get
					Return _comment
				End Get
				Set
					_comment = value
				End Set
			End Property

			''' <summary>
			''' Comentarios mostrados en el cuadro de Texto "Summary Info" o "Properties" de Microsoft Word.
			''' </summary>
			Public Property DocComment() As String
				Get
					Return _doccomm
				End Get
				Set
					_doccomm = value
				End Set
			End Property

			''' <summary>
			''' La direcci�n base usada en las rutas relativas de los enlaces del documento. Puede ser una ruta local o una URL.
			''' </summary>
			Public Property HlinkBase() As String
				Get
					Return _hlinkbase
				End Get
				Set
					_hlinkbase = value
				End Set
			End Property

			''' <summary>
			''' Fecha/Hora de creaci�n del documento.
			''' </summary>
			Public Property CreationTime() As DateTime
				Get
					Return _creatim
				End Get
				Set
					_creatim = value
				End Set
			End Property

			''' <summary>
			''' Fecha/Hora de revisi�n del documento.
			''' </summary>
			Public Property RevisionTime() As DateTime
				Get
					Return _revtim
				End Get
				Set
					_revtim = value
				End Set
			End Property

			''' <summary>
			''' Fecha/Hora de �ltima impresi�n del documento.
			''' </summary>
			Public Property LastPrintTime() As DateTime
				Get
					Return _printim
				End Get
				Set
					_printim = value
				End Set
			End Property

			''' <summary>
			''' Fecha/Hora de �ltima copia del documento.
			''' </summary>
			Public Property BackupTime() As DateTime
				Get
					Return _buptim
				End Get
				Set
					_buptim = value
				End Set
			End Property

			''' <summary>
			''' Versi�n del documento.
			''' </summary>
			Public Property Version() As Integer
				Get
					Return _version
				End Get
				Set
					_version = value
				End Set
			End Property

			''' <summary>
			''' Versi�n interna del documento.
			''' </summary>
			Public Property InternalVersion() As Integer
				Get
					Return _vern
				End Get
				Set
					_vern = value
				End Set
			End Property

			''' <summary>
			''' Tiempo total de edici�n del documento (en minutos).
			''' </summary>
			Public Property EditingTime() As Integer
				Get
					Return _edmins
				End Get
				Set
					_edmins = value
				End Set
			End Property

			''' <summary>
			''' N�mero de p�ginas del documento.
			''' </summary>
			Public Property NumberOfPages() As Integer
				Get
					Return _nofpages
				End Get
				Set
					_nofpages = value
				End Set
			End Property

			''' <summary>
			''' N�mero de palabras del documento.
			''' </summary>
			Public Property NumberOfWords() As Integer
				Get
					Return _nofwords
				End Get
				Set
					_nofwords = value
				End Set
			End Property

			''' <summary>
			''' N�mero de caracteres del documento.
			''' </summary>
			Public Property NumberOfChars() As Integer
				Get
					Return _nofchars
				End Get
				Set
					_nofchars = value
				End Set
			End Property

			''' <summary>
			''' Identificaci�n interna del documento.
			''' </summary>
			Public Property Id() As Integer
				Get
					Return _id
				End Get
				Set
					_id = value
				End Set
			End Property

			#End Region

			#Region "Metodos publicos"

			''' <summary>
			''' Devuelve la representaci�n del nodo en forma de cadena de caracteres.
			''' </summary>
			''' <returns>Representaci�n del nodo en forma de cadena de caracteres.</returns>
			Public Overloads Overrides Function ToString() As String
				Dim str As New StringBuilder()

				str.AppendLine("Title     : " & Me.Title)
				str.AppendLine("Subject   : " & Me.Subject)
				str.AppendLine("Author    : " & Me.Author)
				str.AppendLine("Manager   : " & Me.Manager)
				str.AppendLine("Company   : " & Me.Company)
				str.AppendLine("Operator  : " & Me.[Operator])
				str.AppendLine("Category  : " & Me.Category)
				str.AppendLine("Keywords  : " & Me.Keywords)
				str.AppendLine("Comment   : " & Me.Comment)
				str.AppendLine("DComment  : " & Me.DocComment)
				str.AppendLine("HLinkBase : " & Me.HlinkBase)
				str.AppendLine("Created   : " & Me.CreationTime)
				str.AppendLine("Revised   : " & Me.RevisionTime)
				str.AppendLine("Printed   : " & Me.LastPrintTime)
				str.AppendLine("Backup    : " & Me.BackupTime)
				str.AppendLine("Version   : " & Me.Version)
				str.AppendLine("IVersion  : " & Me.InternalVersion)
				str.AppendLine("Editing   : " & Me.EditingTime)
				str.AppendLine("Num Pages : " & Me.NumberOfPages)
				str.AppendLine("Num Words : " & Me.NumberOfWords)
				str.AppendLine("Num Chars : " & Me.NumberOfChars)
				str.AppendLine("Id        : " & Me.Id)

				Return str.ToString()
			End Function

			#End Region
		End Class
	End Namespace
End Namespace
