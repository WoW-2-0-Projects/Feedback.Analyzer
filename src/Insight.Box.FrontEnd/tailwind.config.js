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
        accentTertiaryColor: '#4B4D63',
        primaryContentColor: '#f5f5f5',
        secondaryContentColor: '#b5b5b5',
        dangerColor: '#E63946',
        successColor: '#2A9D8F',
      },
      fontFamily: {
        'poppins': ['Poppins', 'sans-serif'],
      },
      keyframes: {
        fadeInExpand: {
          '0%': { opacity: '0', height: '0' },
          '50%': { opacity: '1', height: '0' },
          '100%': { opacity: '1', height: 'var(--expand-height)' },
        },
        fadeOutCollapse: {
          '0%': { opacity: '1', height: 'var(--expand-height)' },
          '50%': { opacity: '0', height: 'var(--expand-height)' },
          '100%': { opacity: '0', height: '0' },
        },
        fadeIn: {
          '0%': { opacity: '0',  transform: 'translateY(-20%) translateX(50%)' },
          '100%': { opacity: '1',  transform: 'translateY(-50%) translateX(50%)' },
        },
        fadeOut: {
          '0%': { opacity: '1',  transform: 'translateY(-50%) translateX(50%)'  },
          '100%': { opacity: '0', transform: 'translateY(-20%) translateX(50%)' },
        },
        backgroundFadeIn: {
          '0%': { opacity: '0' },
          '100%': { opacity: '1' },
        },
        backgroundFadeOut: {
          '0%': { opacity: '1' },
          '100%': { opacity: '0' },
        },
      },
      animation: {
        fadeInExpand: 'fadeInExpand 0.5s ease-in-out forwards',
        fadeOutCollapse: 'fadeOutCollapse 0.5s ease-in-out forwards',
        fadeIn: 'fadeIn 0.15s ease-out forwards',
        fadeOut: 'fadeOut 0.15s ease-out forwards',
        backgroundFadeIn: 'backgroundFadeIn 0.3s ease-out forwards',
        backgroundFadeOut: 'backgroundFadeOut 0.3s ease-out forwards',
      },
    },
  },
  plugins: [],
}