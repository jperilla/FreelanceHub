﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class GoogleCustomizations {
    
    private GoogleCustomizationsCustomSearchEngine customSearchEngineField;
    
    private string[] textField;
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngine CustomSearchEngine {
        get {
            return this.customSearchEngineField;
        }
        set {
            this.customSearchEngineField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngine {
    
    private string titleField;
    
    private GoogleCustomizationsCustomSearchEngineContext contextField;
    
    private GoogleCustomizationsCustomSearchEngineLookAndFeel lookAndFeelField;
    
    private object adSenseField;
    
    private GoogleCustomizationsCustomSearchEngineEnterpriseAccount enterpriseAccountField;
    
    private GoogleCustomizationsCustomSearchEngineImageSearchSettings imageSearchSettingsField;
    
    private object autocomplete_settingsField;
    
    private GoogleCustomizationsCustomSearchEngineSort_by_keys[] sort_by_keysField;
    
    private GoogleCustomizationsCustomSearchEngineAnnotations annotationsField;
    
    private string[] textField;
    
    private string languageField;
    
    private bool enable_suggestField;
    
    private string encodingField;
    
    private ulong creatorField;
    
    private string idField;
    
    /// <remarks/>
    public string Title {
        get {
            return this.titleField;
        }
        set {
            this.titleField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineContext Context {
        get {
            return this.contextField;
        }
        set {
            this.contextField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineLookAndFeel LookAndFeel {
        get {
            return this.lookAndFeelField;
        }
        set {
            this.lookAndFeelField = value;
        }
    }
    
    /// <remarks/>
    public object AdSense {
        get {
            return this.adSenseField;
        }
        set {
            this.adSenseField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineEnterpriseAccount EnterpriseAccount {
        get {
            return this.enterpriseAccountField;
        }
        set {
            this.enterpriseAccountField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineImageSearchSettings ImageSearchSettings {
        get {
            return this.imageSearchSettingsField;
        }
        set {
            this.imageSearchSettingsField = value;
        }
    }
    
    /// <remarks/>
    public object autocomplete_settings {
        get {
            return this.autocomplete_settingsField;
        }
        set {
            this.autocomplete_settingsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("sort_by_keys")]
    public GoogleCustomizationsCustomSearchEngineSort_by_keys[] sort_by_keys {
        get {
            return this.sort_by_keysField;
        }
        set {
            this.sort_by_keysField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineAnnotations Annotations {
        get {
            return this.annotationsField;
        }
        set {
            this.annotationsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string language {
        get {
            return this.languageField;
        }
        set {
            this.languageField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool enable_suggest {
        get {
            return this.enable_suggestField;
        }
        set {
            this.enable_suggestField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string encoding {
        get {
            return this.encodingField;
        }
        set {
            this.encodingField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong creator {
        get {
            return this.creatorField;
        }
        set {
            this.creatorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineContext {
    
    private GoogleCustomizationsCustomSearchEngineContextLabel[] backgroundLabelsField;
    
    private string[] textField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Label", IsNullable=false)]
    public GoogleCustomizationsCustomSearchEngineContextLabel[] BackgroundLabels {
        get {
            return this.backgroundLabelsField;
        }
        set {
            this.backgroundLabelsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineContextLabel {
    
    private string modeField;
    
    private string nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string mode {
        get {
            return this.modeField;
        }
        set {
            this.modeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineLookAndFeel {
    
    private object logoField;
    
    private GoogleCustomizationsCustomSearchEngineLookAndFeelColors colorsField;
    
    private GoogleCustomizationsCustomSearchEngineLookAndFeelPromotions promotionsField;
    
    private GoogleCustomizationsCustomSearchEngineLookAndFeelSearchControls searchControlsField;
    
    private GoogleCustomizationsCustomSearchEngineLookAndFeelResults resultsField;
    
    private byte ads_layoutField;
    
    private string promotion_url_lengthField;
    
    private bool enable_cse_thumbnailField;
    
    private string element_brandingField;
    
    private string url_lengthField;
    
    private string text_fontField;
    
    private bool custom_themeField;
    
    private byte themeField;
    
    private byte element_layoutField;
    
    private bool nonprofitField;
    
    /// <remarks/>
    public object Logo {
        get {
            return this.logoField;
        }
        set {
            this.logoField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineLookAndFeelColors Colors {
        get {
            return this.colorsField;
        }
        set {
            this.colorsField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineLookAndFeelPromotions Promotions {
        get {
            return this.promotionsField;
        }
        set {
            this.promotionsField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineLookAndFeelSearchControls SearchControls {
        get {
            return this.searchControlsField;
        }
        set {
            this.searchControlsField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineLookAndFeelResults Results {
        get {
            return this.resultsField;
        }
        set {
            this.resultsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte ads_layout {
        get {
            return this.ads_layoutField;
        }
        set {
            this.ads_layoutField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string promotion_url_length {
        get {
            return this.promotion_url_lengthField;
        }
        set {
            this.promotion_url_lengthField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool enable_cse_thumbnail {
        get {
            return this.enable_cse_thumbnailField;
        }
        set {
            this.enable_cse_thumbnailField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string element_branding {
        get {
            return this.element_brandingField;
        }
        set {
            this.element_brandingField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string url_length {
        get {
            return this.url_lengthField;
        }
        set {
            this.url_lengthField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string text_font {
        get {
            return this.text_fontField;
        }
        set {
            this.text_fontField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool custom_theme {
        get {
            return this.custom_themeField;
        }
        set {
            this.custom_themeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte theme {
        get {
            return this.themeField;
        }
        set {
            this.themeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte element_layout {
        get {
            return this.element_layoutField;
        }
        set {
            this.element_layoutField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool nonprofit {
        get {
            return this.nonprofitField;
        }
        set {
            this.nonprofitField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineLookAndFeelColors {
    
    private string titleField;
    
    private string title_activeField;
    
    private string title_hoverField;
    
    private string visitedField;
    
    private string textField;
    
    private string borderField;
    
    private string backgroundField;
    
    private string urlField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title {
        get {
            return this.titleField;
        }
        set {
            this.titleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title_active {
        get {
            return this.title_activeField;
        }
        set {
            this.title_activeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title_hover {
        get {
            return this.title_hoverField;
        }
        set {
            this.title_hoverField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string visited {
        get {
            return this.visitedField;
        }
        set {
            this.visitedField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string border {
        get {
            return this.borderField;
        }
        set {
            this.borderField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string background {
        get {
            return this.backgroundField;
        }
        set {
            this.backgroundField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string url {
        get {
            return this.urlField;
        }
        set {
            this.urlField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineLookAndFeelPromotions {
    
    private string title_active_colorField;
    
    private string title_hover_colorField;
    
    private string snippet_colorField;
    
    private string border_colorField;
    
    private string background_colorField;
    
    private string url_colorField;
    
    private string title_visited_colorField;
    
    private string title_colorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title_active_color {
        get {
            return this.title_active_colorField;
        }
        set {
            this.title_active_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title_hover_color {
        get {
            return this.title_hover_colorField;
        }
        set {
            this.title_hover_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string snippet_color {
        get {
            return this.snippet_colorField;
        }
        set {
            this.snippet_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string border_color {
        get {
            return this.border_colorField;
        }
        set {
            this.border_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string background_color {
        get {
            return this.background_colorField;
        }
        set {
            this.background_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string url_color {
        get {
            return this.url_colorField;
        }
        set {
            this.url_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title_visited_color {
        get {
            return this.title_visited_colorField;
        }
        set {
            this.title_visited_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title_color {
        get {
            return this.title_colorField;
        }
        set {
            this.title_colorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineLookAndFeelSearchControls {
    
    private string tab_selected_background_colorField;
    
    private string tab_selected_border_colorField;
    
    private string tab_background_colorField;
    
    private string tab_border_colorField;
    
    private string button_background_colorField;
    
    private string button_border_colorField;
    
    private string input_border_colorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tab_selected_background_color {
        get {
            return this.tab_selected_background_colorField;
        }
        set {
            this.tab_selected_background_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tab_selected_border_color {
        get {
            return this.tab_selected_border_colorField;
        }
        set {
            this.tab_selected_border_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tab_background_color {
        get {
            return this.tab_background_colorField;
        }
        set {
            this.tab_background_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tab_border_color {
        get {
            return this.tab_border_colorField;
        }
        set {
            this.tab_border_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string button_background_color {
        get {
            return this.button_background_colorField;
        }
        set {
            this.button_background_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string button_border_color {
        get {
            return this.button_border_colorField;
        }
        set {
            this.button_border_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string input_border_color {
        get {
            return this.input_border_colorField;
        }
        set {
            this.input_border_colorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineLookAndFeelResults {
    
    private string border_colorField;
    
    private string background_colorField;
    
    private string ads_border_colorField;
    
    private string ads_background_colorField;
    
    private string background_hover_colorField;
    
    private string border_hover_colorField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string border_color {
        get {
            return this.border_colorField;
        }
        set {
            this.border_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string background_color {
        get {
            return this.background_colorField;
        }
        set {
            this.background_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ads_border_color {
        get {
            return this.ads_border_colorField;
        }
        set {
            this.ads_border_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ads_background_color {
        get {
            return this.ads_background_colorField;
        }
        set {
            this.ads_background_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string background_hover_color {
        get {
            return this.background_hover_colorField;
        }
        set {
            this.background_hover_colorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string border_hover_color {
        get {
            return this.border_hover_colorField;
        }
        set {
            this.border_hover_colorField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineEnterpriseAccount {
    
    private GoogleCustomizationsCustomSearchEngineEnterpriseAccountAccountAdmin accountAdminField;
    
    private GoogleCustomizationsCustomSearchEngineEnterpriseAccountOrganization organizationField;
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineEnterpriseAccountAccountAdmin AccountAdmin {
        get {
            return this.accountAdminField;
        }
        set {
            this.accountAdminField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineEnterpriseAccountOrganization Organization {
        get {
            return this.organizationField;
        }
        set {
            this.organizationField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineEnterpriseAccountAccountAdmin {
    
    private string job_titleField;
    
    private string countryField;
    
    private ulong phoneField;
    
    private string emailField;
    
    private string last_nameField;
    
    private string first_nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string job_title {
        get {
            return this.job_titleField;
        }
        set {
            this.job_titleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string country {
        get {
            return this.countryField;
        }
        set {
            this.countryField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong phone {
        get {
            return this.phoneField;
        }
        set {
            this.phoneField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string email {
        get {
            return this.emailField;
        }
        set {
            this.emailField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string last_name {
        get {
            return this.last_nameField;
        }
        set {
            this.last_nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string first_name {
        get {
            return this.first_nameField;
        }
        set {
            this.first_nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineEnterpriseAccountOrganization {
    
    private string nameField;
    
    private byte sizeField;
    
    private string typeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte size {
        get {
            return this.sizeField;
        }
        set {
            this.sizeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineImageSearchSettings {
    
    private bool enableField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool enable {
        get {
            return this.enableField;
        }
        set {
            this.enableField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineSort_by_keys {
    
    private string keyField;
    
    private string labelField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string key {
        get {
            return this.keyField;
        }
        set {
            this.keyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string label {
        get {
            return this.labelField;
        }
        set {
            this.labelField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineAnnotations {
    
    private GoogleCustomizationsCustomSearchEngineAnnotationsAnnotation[] annotationField;
    
    private string[] textField;
    
    private byte totalField;
    
    private byte numField;
    
    private byte startField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Annotation")]
    public GoogleCustomizationsCustomSearchEngineAnnotationsAnnotation[] Annotation {
        get {
            return this.annotationField;
        }
        set {
            this.annotationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text {
        get {
            return this.textField;
        }
        set {
            this.textField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte total {
        get {
            return this.totalField;
        }
        set {
            this.totalField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte num {
        get {
            return this.numField;
        }
        set {
            this.numField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte start {
        get {
            return this.startField;
        }
        set {
            this.startField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineAnnotationsAnnotation {
    
    private GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationLabel[] labelField;
    
    private GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationAdditionalData additionalDataField;
    
    private string hrefField;
    
    private string timestampField;
    
    private string aboutField;
    
    private byte scoreField;
    
    private bool scoreFieldSpecified;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Label")]
    public GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationLabel[] Label {
        get {
            return this.labelField;
        }
        set {
            this.labelField = value;
        }
    }
    
    /// <remarks/>
    public GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationAdditionalData AdditionalData {
        get {
            return this.additionalDataField;
        }
        set {
            this.additionalDataField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string href {
        get {
            return this.hrefField;
        }
        set {
            this.hrefField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string timestamp {
        get {
            return this.timestampField;
        }
        set {
            this.timestampField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string about {
        get {
            return this.aboutField;
        }
        set {
            this.aboutField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte score {
        get {
            return this.scoreField;
        }
        set {
            this.scoreField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool scoreSpecified {
        get {
            return this.scoreFieldSpecified;
        }
        set {
            this.scoreFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationLabel {
    
    private string nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GoogleCustomizationsCustomSearchEngineAnnotationsAnnotationAdditionalData {
    
    private string valueField;
    
    private string attributeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string attribute {
        get {
            return this.attributeField;
        }
        set {
            this.attributeField = value;
        }
    }
}
