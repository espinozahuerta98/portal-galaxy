export function generateAndDownloadPdf(htmlOrElement, filename) {
    const doc = new jspdf.jsPDF({
        orientation: '1',
        unit: 'pt',
        format: 'a4'
    });

    return new Promise((resolve, reject) => {
        doc.html(htmlOrElement,
            {
                callback: doc => {
                    doc.save(filename);
                    resolve();
                }
            });
    });
}