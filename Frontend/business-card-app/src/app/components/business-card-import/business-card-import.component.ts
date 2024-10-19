import { Component, ElementRef, ViewChild } from '@angular/core';
import Swal from 'sweetalert2';
import { BusinessCardService } from '../../services/business-card.service';

@Component({
  selector: 'app-business-card-import',
  standalone: true,
  imports: [],
  templateUrl: './business-card-import.component.html',
  styleUrls: ['./business-card-import.component.css']
})
export class BusinessCardImportComponent {
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  constructor(private businessCardService: BusinessCardService) {}

  previewUrl: string | null = null;
  bulkFile: File | null = null;
  maxFileSize = 1 * 1024 * 1024;
  acceptedFileTypes = ['text/csv','text/xml', 'application/xml', 'image/jpeg', 'image/png'];


  handleBulkFileInput(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.validateFile(file);
    }
  }


  onBulkFileDropped(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();

    if (event.dataTransfer && event.dataTransfer.files.length > 0) {
      const fileList = event.dataTransfer.files;


      const fileInputElement = this.fileInput.nativeElement;
      const dataTransfer = new DataTransfer();

      Array.from(fileList).forEach(file => dataTransfer.items.add(file));
      fileInputElement.files = dataTransfer.files;


      this.handleBulkFileInput({ target: fileInputElement } as unknown as Event);
    }
  }
  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
  }
  validateFile(file: File) {
    if (!this.acceptedFileTypes.includes(file.type)) {
      Swal.fire({
        toast: true,
        icon: 'error',
        title: 'Invalid file type. Please upload a valid file.',
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
      });
      this.bulkFile = null;
      return;
    }


    if (file.size > this.maxFileSize) {
      Swal.fire({
        toast: true,
        icon: 'error',
        title: 'File size exceeds 1 MB. Please upload a smaller file.',
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
      });
      this.bulkFile = null;
    } else {
      this.bulkFile = file;
    }
  }

  submitBulkFile() {
    if (this.bulkFile) {
      this.businessCardService.importBusinessCards(this.bulkFile).subscribe({
        next: (response) => {
          Swal.fire({
            toast: true,
            icon: 'success',
            title: 'Business cards imported successfully!',
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
          });
        },
        error: (error) => {
          console.error('Error uploading file:', error);
          Swal.fire({
            toast: true,
            icon: 'error',
            title: 'An error occurred while uploading the file.',
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
          });
        }
      });
    } else {
      Swal.fire({
        toast: true,
        icon: 'warning',
        title: 'Please select a file before submitting.',
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
      });
    }
  }
}
