import type { Metadata } from 'next';
import type { FC, PropsWithChildren } from 'react';

export const metadata: Metadata = {
  title: 'Degrees | Next Degree',
};

const DegreesLayout: FC<Required<PropsWithChildren>> = ({ children }) => {
  return children;
};

export default DegreesLayout;
