import { FormControl } from '@angular/forms';
import Swal from 'sweetalert2';
export const AllowedExtensionsValidator = (control: FormControl) => {
  const allowedExtensions = [
    'pdf',
    'jpeg',
    'png',
    'mp4',
    'txt',
    'doc',
    'xls',
    'ppt',
    'gif',
    'mp3',
    'rar',
    'zip',
  ];
  const fileName: string = control.value ? control.value.name : '';

  if (fileName) {
    const extension = fileName.split('.').pop()?.toLowerCase();
    if (!allowedExtensions.includes(extension)) {
      return { notValidEXT: true };
    }
  }
  return null;
};
