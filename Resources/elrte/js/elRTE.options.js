/*
 * elRTE configuration
 *
 * @param doctype         - doctype for editor iframe
 * @param cssClass        - css class for editor
 * @param cssFiles        - array of css files, witch will inlude in iframe
 * @param height          - not used now (may be deleted in future)
 * @param lang            - interface language (requires file in i18n dir)
 * @param toolbar         - name of toolbar to load
 * @param absoluteURLs    - convert files and images urls to absolute or not
 * @param allowSource     - is source editing allowing
 * @param stripWhiteSpace - strip лишние whitespaces/tabs or not
 * @param styleWithCSS    - use style=... instead of strong etc.
 * @param fmAllow         - allow using file manger (elFinder)
 * @param fmOpen          - callback for open file manager
 * @param buttons         - object with pairs of buttons classes names and titles (when create new button, you have to add iys name here)
 * @param panels          - named groups of buttons
 * @param panelNames      - title of panels (required for one planned feature)
 * @param toolbars        - named redy to use toolbals (you may combine your own toolbar)
 *
 * @author:    Dmitry Levashov (dio) dio@std42.ru
 * Copyright: Studio 42, http://www.std42.ru
 */
(function($) {
elRTE.prototype.options   = {
	doctype         : '<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">',
	cssClass        : 'el-rte',
	cssfiles        : [],
	height          : null,
	resizable       : true,
	lang            : 'en',
	toolbar         : 'normal',
	absoluteURLs    : true,
	allowSource     : true,
	stripWhiteSpace : true,
	styleWithCSS    : false,
	fmAllow         : true,
	fmOpen          : null,
	/* if set all other tag will be removed */
	allowTags : [],
	/* if set this tags will be removed */
	denyTags : ['applet', 'base', 'basefont', 'bgsound', 'blink', 'body', 'col', 'colgroup', 'isindex', 'frameset', 'html', 'head', 'meta', 'marquee', 'noframes', 'noembed', 'o:p', 'title', 'xml'],
	denyAttr : [],
	/* on paste event this attributes will removed from pasted html */
	pasteDenyAttr : ['id', 'name', 'class', 'style', 'language', 'onclick', 'ondblclick', 'onhover', 'onkeup', 'onkeydown', 'onkeypress'],
	/* If false - all text nodes will be wrapped by paragraph tag */
	allowTextNodes : true,
	/* allow browser specific styles like -moz|-webkit|-o */
	allowBrowsersSpecStyles : false,
	/* allow paste content into editor */
	allowPaste : true,
	/* if true - only text will be pasted (not in ie) */
	pasteOnlyText : false,
	/* user replacement rules */
	replace : [],
	/* user restore rules */
	restore : [],
	pagebreak : '<div style="page-break-after: always;"></div>', //'<!-- pagebreak -->',
	buttons         : {
		'save'                : 'Save',
//		'copy'                : 'Copy',
//		'cut'                 : 'Cut',
//		'css'                 : 'Css style and class',
//		'paste'               : 'Paste',
//		'pastetext'           : 'Paste only text',
//		'pasteformattext'     : 'Paste formatted text',
//		'removeformat'        : 'Clean format', 
//		'undo'                : 'Undo last action',
//		'redo'                : 'Redo previous action',
		'bold'                : 'Bold',
		'italic'              : 'Italic',
		'underline'           : 'Underline',
		'strikethrough'       : 'Strikethrough',
	//	'superscript'         : 'Superscript',
	//	'subscript'           : 'Subscript',
//		'justifyleft'         : 'Align left',
//		'justifyright'        : 'Ailgn right',
//		'justifycenter'       : 'Align center',
//		'justifyfull'         : 'Align full',
//		'indent'              : 'Indent',
//		'outdent'             : 'Outdent',
//		'rtl' : 'Right to left',
//		'ltr' : 'Left to right',
		'forecolor'           : 'Font color',
		'hilitecolor'         : 'Background color',
	//	'formatblock'         : 'Format',
	//	'fontsize'            : 'Font size',
		'fontname'            : 'Font',
		'insertorderedlist'   : 'Ordered list',
		'insertunorderedlist' : 'Unordered list',
//		'horizontalrule'      : 'Horizontal rule',
//		'blockquote'          : 'Blockquote',
//		'div'                 : 'Block element (DIV)',
		'link'                : 'Link',
		'unlink'              : 'Delete link',
		'anchor'              : 'Bookmark',
	//	'image'               : 'Image',
		//'pagebreak'           : 'Page break',
		'smiley'              : 'Smiley',
		'flash'               : 'Flash',
//		'table'               : 'Table',
//		'tablerm'             : 'Delete table',
//		'tableprops'          : 'Table properties',
//		'tbcellprops'         : 'Table cell properties',
//		'tbrowbefore'         : 'Insert row before',
//		'tbrowafter'          : 'Insert row after',
//		'tbrowrm'             : 'Delete row',
//		'tbcolbefore'         : 'Insert column before',
//		'tbcolafter'          : 'Insert column after',
//		'tbcolrm'             : 'Delete column',
//		'tbcellsmerge'        : 'Merge table cells',
//		'tbcellsplit'         : 'Split table cell',
//		'docstructure'        : 'Toggle display document structure',		
		//'fullscreen'          : 'Toggle full screen mode',
		//'nbsp'                : 'Non breakable space',
	//	'stopfloat'           : 'Stop element floating'
		//'about'               : 'About this software'
	},
	panels      : {
		eol        : [], // special panel, insert's a new line in toolbar
		save       : ['save'],
		copypaste  : [],
		undoredo   : ['undo', 'redo'],
        colors     : ['forecolor', 'hilitecolor'],
		style      : ['bold', 'italic', 'underline', 'strikethrough'],
		alignment  : [],
		indent     : [],
		format     : ['fontname'],
		lists      : ['insertorderedlist', 'insertunorderedlist'],
		elements   : ['smiley'],
		direction  : [],
		links      : ['link', 'unlink', 'anchor'],
		images     : ['image'],
		media      : ['image'],		
		tables     : [],		
		fullscreen : []
	},
	toolbars    : {
		tiny     : ['style'],
		compact  : ['save', 'undoredo', 'style', 'alignment', 'lists', 'links', 'fullscreen'],
		normal   : ['save', 'copypaste', 'undoredo', 'style', 'alignment', 'colors', 'indent', 'lists', 'links', 'elements', 'images', 'fullscreen'],
		complete : ['save', 'copypaste', 'undoredo', 'style', 'alignment', 'colors', 'format', 'indent', 'lists', 'links', 'elements', 'media', 'fullscreen'],
		maxi     : ['save', 'undoredo', 'colors', 'style',  'lists',  'format'],
		eldorado : ['save', 'copypaste',  'undoredo', 'style', 'alignment', 'colors', 'format', 'indent', 'lists', 'links', 'elements', 'media', 'tables', 'fullscreen']
		
	},
	panelNames : {
		save      : 'Save',		
		undoredo  : 'Undo/Redo',
        colors    : 'Colors',
		style     : 'Text styles',
		alignment : 'Alignment',
		indent    : 'Indent/Outdent',
		format    : 'Text format',
		lists     : 'Lists',		
		direction : 'Script direction',		
		images    : 'Images',
		media     : 'Media'
		
		
	}
};
})(jQuery);