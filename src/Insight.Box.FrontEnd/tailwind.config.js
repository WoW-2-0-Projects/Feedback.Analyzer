export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    screens: {
      md: '745px',
      lg: '950px',
      xl: '1440px',
    },
    extend: {
      colors: {
        primaryColor: '#454861',
        secondaryColor: '#555979',
        tertiaryColor: '#9A9CAB',
        accentPrimaryColor: '#A07C7F',
        accentSecondaryColor: '#656a80',
        primaryContentColor: '#f5f5f5',
        dangerColor: '#E63946',
        successColor: '#2A9D8F',

        // bgColor1: '#555979',
        // bgColor2: '#454861',
        // textColorPrimary: '#f5f5f5',
        // textColorSecondary: '#b5b5b5',
        // actionPrimaryColor: '#A07C7F',
        // actionDangerColor: '#E63946',
        // actionSuccessColor: '#2A9D8F',
        // actionSecondaryColor: '#A07C7F',
        // actionDisabledColor: '#A07C7F',
      },
      fontFamily: {
        'poppins': ['Poppins', 'sans-serif'],
      },
    },
  },
  plugins: [],
}