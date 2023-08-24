namespace GoogleTrendsApi;

public enum DateOptions
{
    [Description("now 1-H")]
    LastHour,
    [Description("now 4-H")]
    LastFourHours,
    [Description("now 1-d")]
    LastDay,
    [Description("now 7-d")]
    LastWeek,
    [Description("today 1-m")]
    LastMonth,
    [Description("today 3-m")]
    LastThreeMonths,
    [Description("today 12-m")]
    LastYear,
    [Description("today 5-y")]
    LastFiveYears,
    [Description("all")]
    FromStart,
}

public enum GroupOptions
{
    [Description("")]
    All,
    [Description("images")]
    images,
    [Description("news")]
    news,
    [Description("youtube")]
    youtube,
    [Description("froogle")]
    froogle
}
 
public enum Category
{
    [Description("all")]
    All,
    [Description("e")]
    Entertainment,
    [Description("b")]
    Business,
    [Description("t")]
    ScienceAndTech,
    [Description("m")]
    Health,
    [Description("s")]
    Sports,
    [Description("h")]
    TopStories,
}

public enum GeoId
{
    [Description("")]
    WorldWide,

    [Description("AF")]
    Afghanistan,

    [Description("AX")]
    AlandIslands,

    [Description("AL")]
    Albania,

    [Description("DZ")]
    Algeria,

    [Description("AS")]
    AmericanSamoa,

    [Description("AD")]
    Andorra,

    [Description("AO")]
    Angola,

    [Description("AI")]
    Anguilla,

    [Description("AQ")]
    Antarctica,

    [Description("AG")]
    AntiguaBarbuda,

    [Description("AR")]
    Argentina,

    [Description("AM")]
    Armenia,

    [Description("AW")]
    Aruba,

    [Description("AU")]
    Australia,

    [Description("AT")]
    Austria,

    [Description("AZ")]
    Azerbaijan,

    [Description("BS")]
    Bahamas,

    [Description("BH")]
    Bahrain,

    [Description("BD")]
    Bangladesh,

    [Description("BB")]
    Barbados,

    [Description("BY")]
    Belarus,

    [Description("BE")]
    Belgium,

    [Description("BZ")]
    Belize,

    [Description("BJ")]
    Benin,

    [Description("BM")]
    Bermuda,

    [Description("BT")]
    Bhutan,

    [Description("BO")]
    Bolivia,

    [Description("BA")]
    BosniaHerzegovina,

    [Description("BW")]
    Botswana,

    [Description("BV")]
    BouvetIsland,

    [Description("BR")]
    Brazil,

    [Description("IO")]
    BritishIndianOceanTerritory,

    [Description("VG")]
    BritishVirginIslands,

    [Description("BN")]
    Brunei,

    [Description("BG")]
    Bulgaria,

    [Description("BF")]
    BurkinaFaso,

    [Description("BI")]
    Burundi,

    [Description("KH")]
    Cambodia,

    [Description("CM")]
    Cameroon,

    [Description("CA")]
    Canada,

    [Description("CV")]
    CapeVerde,

    [Description("BQ")]
    CaribbeanNetherlands,

    [Description("KY")]
    CaymanIslands,

    [Description("CF")]
    CentralAfricanRepublic,

    [Description("TD")]
    Chad,

    [Description("CL")]
    Chile,

    [Description("CN")]
    China,

    [Description("CX")]
    ChristmasIsland,

    [Description("CC")]
    CocosKeelingIslands,

    [Description("CO")]
    Colombia,

    [Description("KM")]
    Comoros,

    [Description("CG")]
    CongoBrazzaville,

    [Description("CD")]
    CongoKinshasa,

    [Description("CK")]
    CookIslands,

    [Description("CR")]
    CostaRica,

    [Description("CI")]
    CotedIvoire,

    [Description("HR")]
    Croatia,

    [Description("CU")]
    Cuba,

    [Description("CW")]
    Curacao,

    [Description("CY")]
    Cyprus,

    [Description("CZ")]
    Czechia,

    [Description("DK")]
    Denmark,

    [Description("DJ")]
    Djibouti,

    [Description("DM")]
    Dominica,

    [Description("DO")]
    DominicanRepublic,

    [Description("EC")]
    Ecuador,

    [Description("EG")]
    Egypt,

    [Description("SV")]
    ElSalvador,

    [Description("GQ")]
    EquatorialGuinea,

    [Description("ER")]
    Eritrea,

    [Description("EE")]
    Estonia,

    [Description("SZ")]
    Eswatini,

    [Description("ET")]
    Ethiopia,

    [Description("FK")]
    FalklandIslandsIslasMalvinas,

    [Description("FO")]
    FaroeIslands,

    [Description("FJ")]
    Fiji,

    [Description("FI")]
    Finland,

    [Description("FR")]
    France,

    [Description("GF")]
    FrenchGuiana,

    [Description("PF")]
    FrenchPolynesia,

    [Description("TF")]
    FrenchSouthernTerritories,

    [Description("GA")]
    Gabon,

    [Description("GM")]
    Gambia,

    [Description("GE")]
    Georgia,

    [Description("DE")]
    Germany,

    [Description("GH")]
    Ghana,

    [Description("GI")]
    Gibraltar,

    [Description("GR")]
    Greece,

    [Description("GL")]
    Greenland,

    [Description("GD")]
    Grenada,

    [Description("GP")]
    Guadeloupe,

    [Description("GU")]
    Guam,

    [Description("GT")]
    Guatemala,

    [Description("GG")]
    Guernsey,

    [Description("GN")]
    Guinea,

    [Description("GW")]
    GuineaBissau,

    [Description("GY")]
    Guyana,

    [Description("HT")]
    Haiti,

    [Description("HM")]
    HeardMcDonaldIslands,

    [Description("HN")]
    Honduras,

    [Description("HK")]
    HongKong,

    [Description("HU")]
    Hungary,

    [Description("IS")]
    Iceland,

    [Description("IN")]
    India,

    [Description("ID")]
    Indonesia,

    [Description("IR")]
    Iran,

    [Description("IQ")]
    Iraq,

    [Description("IE")]
    Ireland,

    [Description("IM")]
    IsleofMan,

    [Description("IL")]
    Israel,

    [Description("IT")]
    Italy,

    [Description("JM")]
    Jamaica,

    [Description("JP")]
    Japan,

    [Description("JE")]
    Jersey,

    [Description("JO")]
    Jordan,

    [Description("KZ")]
    Kazakhstan,

    [Description("KE")]
    Kenya,

    [Description("KI")]
    Kiribati,

    [Description("XK")]
    Kosovo,

    [Description("KW")]
    Kuwait,

    [Description("KG")]
    Kyrgyzstan,

    [Description("LA")]
    Laos,

    [Description("LV")]
    Latvia,

    [Description("LB")]
    Lebanon,

    [Description("LS")]
    Lesotho,

    [Description("LR")]
    Liberia,

    [Description("LY")]
    Libya,

    [Description("LI")]
    Liechtenstein,

    [Description("LT")]
    Lithuania,

    [Description("LU")]
    Luxembourg,

    [Description("MO")]
    Macao,

    [Description("MG")]
    Madagascar,

    [Description("MW")]
    Malawi,

    [Description("MY")]
    Malaysia,

    [Description("MV")]
    Maldives,

    [Description("ML")]
    Mali,

    [Description("MT")]
    Malta,

    [Description("MH")]
    MarshallIslands,

    [Description("MQ")]
    Martinique,

    [Description("MR")]
    Mauritania,

    [Description("MU")]
    Mauritius,

    [Description("YT")]
    Mayotte,

    [Description("MX")]
    Mexico,

    [Description("FM")]
    Micronesia,

    [Description("MD")]
    Moldova,

    [Description("MC")]
    Monaco,

    [Description("MN")]
    Mongolia,

    [Description("ME")]
    Montenegro,

    [Description("MS")]
    Montserrat,

    [Description("MA")]
    Morocco,

    [Description("MZ")]
    Mozambique,

    [Description("MM")]
    MyanmarBurma,

    [Description("NA")]
    Namibia,

    [Description("NR")]
    Nauru,

    [Description("NP")]
    Nepal,

    [Description("NL")]
    Netherlands,

    [Description("NC")]
    NewCaledonia,

    [Description("NZ")]
    NewZealand,

    [Description("NI")]
    Nicaragua,

    [Description("NE")]
    Niger,

    [Description("NG")]
    Nigeria,

    [Description("NU")]
    Niue,

    [Description("NF")]
    NorfolkIsland,

    [Description("KP")]
    NorthKorea,

    [Description("MK")]
    NorthMacedonia,

    [Description("MP")]
    NorthernMarianaIslands,

    [Description("NO")]
    Norway,

    [Description("OM")]
    Oman,

    [Description("PK")]
    Pakistan,

    [Description("PW")]
    Palau,

    [Description("PS")]
    Palestine,

    [Description("PA")]
    Panama,

    [Description("PG")]
    PapuaNewGuinea,

    [Description("PY")]
    Paraguay,

    [Description("PE")]
    Peru,

    [Description("PH")]
    Philippines,

    [Description("PN")]
    PitcairnIslands,

    [Description("PL")]
    Poland,

    [Description("PT")]
    Portugal,

    [Description("PR")]
    PuertoRico,

    [Description("QA")]
    Qatar,

    [Description("RE")]
    Reunion,

    [Description("RO")]
    Romania,

    [Description("RU")]
    Russia,

    [Description("RW")]
    Rwanda,

    [Description("WS")]
    Samoa,

    [Description("SM")]
    SanMarino,

    [Description("ST")]
    SaoTomePrincipe,

    [Description("SA")]
    SaudiArabia,

    [Description("SN")]
    Senegal,

    [Description("RS")]
    Serbia,

    [Description("SC")]
    Seychelles,

    [Description("SL")]
    SierraLeone,

    [Description("SG")]
    Singapore,

    [Description("SX")]
    SintMaarten,

    [Description("SK")]
    Slovakia,

    [Description("SI")]
    Slovenia,

    [Description("SB")]
    SolomonIslands,

    [Description("SO")]
    Somalia,

    [Description("ZA")]
    SouthAfrica,

    [Description("GS")]
    SouthGeorgiaSouthSandwichIslands,

    [Description("KR")]
    SouthKorea,

    [Description("SS")]
    SouthSudan,

    [Description("ES")]
    Spain,

    [Description("LK")]
    SriLanka,

    [Description("BL")]
    StBarthelemy,

    [Description("SH")]
    StHelena,

    [Description("KN")]
    StKittsNevis,

    [Description("LC")]
    StLucia,

    [Description("MF")]
    StMartin,

    [Description("PM")]
    StPierreMiquelon,

    [Description("VC")]
    StVincentGrenadines,

    [Description("SD")]
    Sudan,

    [Description("SR")]
    Suriname,

    [Description("SJ")]
    SvalbardJanMayen,

    [Description("SE")]
    Sweden,

    [Description("CH")]
    Switzerland,

    [Description("SY")]
    Syria,

    [Description("TW")]
    Taiwan,

    [Description("TJ")]
    Tajikistan,

    [Description("TZ")]
    Tanzania,

    [Description("TH")]
    Thailand,

    [Description("TL")]
    TimorLeste,

    [Description("TG")]
    Togo,

    [Description("TK")]
    Tokelau,

    [Description("TO")]
    Tonga,

    [Description("TT")]
    TrinidadTobago,

    [Description("TN")]
    Tunisia,

    [Description("TR")]
    Turkey,

    [Description("TM")]
    Turkmenistan,

    [Description("TC")]
    TurksCaicosIslands,

    [Description("TV")]
    Tuvalu,

    [Description("UM")]
    USOutlyingIslands,

    [Description("VI")]
    USVirginIslands,

    [Description("UG")]
    Uganda,

    [Description("UA")]
    Ukraine,

    [Description("AE")]
    UnitedArabEmirates,

    [Description("GB")]
    UnitedKingdom,

    [Description("US")]
    UnitedStates,

    [Description("UY")]
    Uruguay,

    [Description("UZ")]
    Uzbekistan,

    [Description("VU")]
    Vanuatu,

    [Description("VA")]
    VaticanCity,

    [Description("VE")]
    Venezuela,

    [Description("VN")]
    Vietnam,

    [Description("WF")]
    WallisFutuna,

    [Description("EH")]
    WesternSahara,

    [Description("YE")]
    Yemen,

    [Description("ZM")]
    Zambia,

    [Description("ZW")]
    Zimbabwe
}