import cv2
import pytesseract
import os

TESS_PATH = "tessbin"
os.environ["TESSDATA_PREFIX"] = TESS_PATH + "\\tessdata\\"
pytesseract.pytesseract.tesseract_cmd = TESS_PATH + "\\tesseract"

class ImageParser:
    def __init__(self):
        self.resize_scale = 5
    
    def parse_screenshot(self, fullscreen_image):
        crops = self.crop_fullscreen(fullscreen_image)
        for crop in crops:
            print("=" * 5, crop[0], "=" * 5)
            data = self.text_from_image(crop[1])
            print(data + "\n")
    
    def crop_fullscreen(self, fullscreen_image):
        if type(fullscreen_image) is str:
            fullscreen_image = cv2.imread(fullscreen_image)
        crops = []
        b_crop = fullscreen_image[130:335, 1260:1540]
        crops.append(("Ballistics", b_crop))
        return crops
    
    def text_from_image(self, image):
        if type(image) is str:
            image = cv2.imread(image)
        bw = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        dim = (int(bw.shape[0] * (self.resize_scale + 4)), int(bw.shape[1] * self.resize_scale))
        resize = cv2.resize(bw, dim, interpolation=cv2.INTER_AREA)
        data = pytesseract.image_to_string(resize, config="--psm 6")
        return data

parser = ImageParser()
parser.parse_screenshot("RobloxScreenShot20230302_200920013.png")
f = input()
print(f)