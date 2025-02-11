import type { Metadata } from 'next';
import type { FC, PropsWithChildren } from 'react';

export const metadata: Metadata = {
  title: 'Register | Next Degree',
};

const RegisterLayout: FC<Required<PropsWithChildren>> = ({ children }) => {
  return children;
};

export default RegisterLayout;
