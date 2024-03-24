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
        primaryColor: '#232429',
        secondaryColor: '#333740',
        tertiaryColor: '#40434D',
        accentPrimaryColor: '#7E7CF7',
        accentSecondaryColor: '#656a80',
        accentTertiaryColor: '#4B4D63',
        primaryContentColor: '#f5f5f5',
        secondaryContentColor: '#b5b5b5',
        tertiaryContentColor: '#999fab',
        dangerColor: '#E63946',
        successColor: '#73F74B',
        warningColor: '#F1C40F',
        processingColor: '#3498DB',
      },
      fontFamily: {
        'poppins': ['Poppins', 'sans-serif'],
      },
      keyframes: {
        fadeInExpand: {
          '0%': { opacity: '0', maxHeight: '0' },
          '50%': { opacity: '1', maxHeight: '0' },
          '100%': { opacity: '1', maxHeight: 'var(--expand-height)' },
        },
        fadeOutCollapse: {
          '0%': { opacity: '1', maxHeight: 'var(--expand-height)' },
          '50%': { opacity: '0', maxHeight: 'var(--expand-height)' },
          '100%': { opacity: '0', maxHeight: '0' },
        },
        fadeIn: {
          '0%': { opacity: '0', transform: 'translateY(-20%) translateX(-50%)' },
          '100%': { opacity: '1', transform: 'translateY(-50%) translateX(-50%)' },
        },
        fadeOut: {
          '0%': { opacity: '1', transform: 'translateY(-50%) translateX(-50%)' },
          '100%': { opacity: '0', transform: 'translateY(-20%) translateX(-50%)' },
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
      boxShadow: {
        custom: '0px 10px 30px rgba(0, 0, 0, 0.03)',
      }
    },
  },
  plugins: [],
}