import clsx from 'clsx';
import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import './globals.css';
import { FC, PropsWithChildren } from 'react';

const inter = Inter({
  subsets: ['latin'],
});

export const metadata: Metadata = {
  title: 'Next Degree',
};

const RootLayout: FC<Required<PropsWithChildren>> = ({
  children,
}) => {
  return (
    <html lang="en">
    <body
      className={clsx([inter.className, 'antialiased', 'bg-background-primary', 'text-text-primary'])}
    >
    {children}
    </body>
    </html>
  );
};

export default RootLayout;
