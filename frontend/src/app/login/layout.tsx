import type { Metadata } from 'next';
import type { FC, PropsWithChildren } from 'react';

export const metadata: Metadata = {
  title: 'Login | Next Degree',
};

const LoginLayout: FC<Required<PropsWithChildren>> = ({ children }) => {
  return children;
};

export default LoginLayout;
