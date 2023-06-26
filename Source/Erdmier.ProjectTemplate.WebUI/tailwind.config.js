module.exports = {
    content: [
        './**/*.{cshtml,razor}',
        './**/AccountManagePagesNavUtil.cs'
    ],
    theme:   {
        extend: {
            borderWidth: {
                3: '3px'
            },
            boxShadow:   {
                dialog: '0 5px 30px rgb(0 0 0 / 20%)',
                card:   '0 3px 12px rgb(0 0 0 / 20%)'
            },
            colors:      {
                primary:    '#004aad',
                secondary:  '#96c4ff',
                accentOne:  '#d13173',
                accentTwo:  '#dc4d01',
                background: '#f7f7f7'
            },
            fontFamily:  {
                sans: [
                    'Montserrat', 'sans-serif'
                ]
            },
            maxWidth:    {
                108: '27rem',
                240: '60rem'
            },
            minHeight:   {
                20: '5rem'
            },
            screens:     {
                'mobileSm':   '320px',
                'mobileMd':   '375px',
                'mobileLg':   '425px',
                'tabletSm':   '640px',
                'tabletLg':   '768px',
                'laptopSm':   '1024px',
                'laptopMd':   '1280px',
                'laptopLg':   '1440px',
                'desktop':    '1640px',
                '4k':         '2560px',
                'emMobileSm': '20em', // the same prior screen sizes in em units
                'emMobileMd': '23.4375em',
                'emMobileLg': '26.5625em',
                'emTabletSm': '40em',
                'emTabletLg': '48em',
                'emLaptopSm': '64em',
                'emLaptopMd': '80em',
                'emLaptopLg': '90em',
                'emDesktop':  '102em',
                'em4K':       '160em'
            },
            spacing:     {
                66:                    '16.5rem',
                108:                   '27rem',
                200:                   '50rem',
                220:                   '55rem',
                240:                   '60rem',
                'sitePaddingMobileSm': '2.5rem',
                'sitePaddingMobileMd': '3.5rem',
                'sitePaddingMobileLg': '4.5rem',
                'sitePaddingTabletSm': '5.5rem',
                'sitePaddingTabletLg': '6.5rem',
                'sitePaddingLaptopSm': '6.5rem',
                'sitePaddingLaptopMd': '8.5rem',
                'sitePaddingLaptopLg': '9.5rem',
                'sitePaddingDesktop':  '10.5rem',
                'sitePadding4K':       '11.5rem'

            }
        }
    },
    plugins: [
        require('tailwindcss-textshadow')
    ]
};
