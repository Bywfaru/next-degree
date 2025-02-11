import type { Config } from 'tailwindcss';

export default {
  content: [
    './src/pages/**/*.{js,ts,jsx,tsx,mdx}',
    './src/components/**/*.{js,ts,jsx,tsx,mdx}',
    './src/app/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    extend: {
      colors: {
        'background-primary': '#ECE8EF',
        primary: '#4392F1',
        secondary: '#E3EBFF',
        accent: '#DC493A',
        'text-primary': '#1E1E1E',
        'grade-good': '#37A52E',
        'grade-fair': '#E98223',
        'grade-poor': '#F16363',
        'course-completed': '#88EF64',
        'course-in-progress': '#EFBC59',
      },
    },
  },
  plugins: [],
} satisfies Config;
