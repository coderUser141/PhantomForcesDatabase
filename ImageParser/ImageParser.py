from queue import Full
import sys
sys.path.append('C:\\Users\\peter\\AppData\\Local\\Packages\\pythonsoftwarefoundation.python.3.9_qbz5n2kfra8p0\\localcache\\local-packages\\python39\\site-packages')

import numpy
import cv2
import pytesseract
import os



# 1560, 300  \ 1840, 630
#   -> 300:330, 1560:280 
# 1260, 400  \  1540, 765
#   -> 400:365, 1260:280

TESS_PATH = sys.argv[2] + "tessbin"
os.environ["TESSDATA_PREFIX"] = TESS_PATH + "\\tessdata\\"
pytesseract.pytesseract.tesseract_cmd = TESS_PATH + "\\tesseract"

def unsharp_mask(image, kernel_size=(5, 5), sigma=1.0, amount=8.0, threshold=0.3):
    """Return a sharpened version of the image, using an unsharp mask."""
    #print(type(image))
    blurred = cv2.GaussianBlur(image, kernel_size, sigma)
    sharpened = float(amount + 1) * image - float(amount) * blurred
    sharpened = numpy.maximum(sharpened, numpy.zeros(sharpened.shape))
    sharpened = numpy.minimum(sharpened, 255 * numpy.ones(sharpened.shape))
    sharpened = sharpened.round().astype(numpy.uint8)
    if threshold > 0:
        low_contrast_mask = numpy.absolute(image - blurred) < threshold
        numpy.copyto(sharpened, image, where=low_contrast_mask)
    return sharpened

def thin_font(image):
    kernel = numpy.ones((3,3),numpy.uint8)
    erode = cv2.erode(image, kernel, iterations=1)
    return (erode)


class ImageParser:
    def __init__(self):
        self.resize_scale = 5
    
    def parse_screenshot(self, fullscreen_image, primary, melee):
        crops = self.crop_fullscreen(fullscreen_image, primary, melee)
        for crop in crops:
            print("=" * 5, crop[0], "=" * 5)
            data = ""

            if crop[0] == "General2":
                data = self.text_from_image(crop[1], "true", melee)
            data = self.text_from_image(crop[1], "false", melee)

            print(data + "\n")
            #file1 = open(r"dataOut.txt","w")
            #file1.write(data)
    
    def crop_fullscreen(self, fullscreen_image, primary, melee):
        if type(fullscreen_image) is str:
            fullscreen_image = cv2.imread(fullscreen_image)

        crops = []
        

        if primary == "True": #primaries
            
            w_crop = fullscreen_image[160:370,1560:1840]
            y_crop = fullscreen_image[430:640, 1560:1840]

            v_crop = fullscreen_image[70:280, 1560:1840]
            b_crop = fullscreen_image[130:335, 1260:1540]
            a_crop = fullscreen_image[380:585, 1260:1540]
            c_crop = fullscreen_image[580:785, 1260:1540]
            
            crops.append(("Top", v_crop))
            crops.append(("General1", w_crop))
            crops.append(("General2", y_crop))
            crops.append(("Ballistics", b_crop))
            crops.append(("Advanced1", a_crop))
            crops.append(("Advanced2", c_crop))

            print("hi im a primary\n")

        elif primary == "False": #secondaries

            w_crop = fullscreen_image[160:350, 1560:1840]
            y_crop = fullscreen_image[420:600, 1560:1840]
            
            v_crop = fullscreen_image[70:280, 1560:1840]
            b_crop = fullscreen_image[130:335, 1260:1540]
            a_crop = fullscreen_image[380:585, 1260:1540]
            c_crop = fullscreen_image[580:785, 1260:1540]
            
            crops.append(("Top", v_crop))
            crops.append(("General1", w_crop))
            crops.append(("General2", y_crop))
            crops.append(("Ballistics", b_crop))
            crops.append(("Advanced1", a_crop))
            crops.append(("Advanced2", c_crop))

            print("hi im a secondary\n")

        else:
            if melee == "True": #melees
                #do something
                s_crop = fullscreen_image[0:180,1560:1840]
                k_crop = fullscreen_image[180:380,1560:1840]
                x_crop = fullscreen_image[380:580,1560:1840]
                
                crops.append(("Top", s_crop))
                crops.append(("General1", k_crop))
                crops.append(("General2", x_crop))

            elif melee == "False": #grenades
                r_crop = fullscreen_image[80:260, 1560:1840]
                j_crop = fullscreen_image[215:430, 1560:1840] #can be changed to 230
                
                crops.append(("Top", r_crop))
                crops.append(("General", j_crop))
        

        proofread_crop = fullscreen_image[20:800, 1200:1880]
        cv2.imwrite(("cropped__"+sys.argv[6]+"__"+sys.argv[5]),proofread_crop)
        #160:370
        

        #500:700
        return crops

    
    
    def text_from_image(self, image, general2, melee):
        if type(image) is str:
            image = cv2.imread(image)
        print(type(image))
        if image.size is 0:
            return "[OCR] Empty Image"
        bw = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        dim = (int(bw.shape[0] * (self.resize_scale + 4)), int(bw.shape[1] * self.resize_scale))
        resize = cv2.resize(bw, dim, interpolation=cv2.INTER_AREA)

        #process = unsharp_mask(resize)
        process1 = cv2.cvtColor( resize, cv2.COLOR_GRAY2BGR)

        lab= cv2.cvtColor(process1, cv2.COLOR_BGR2LAB)
        l_channel, a, b = cv2.split(lab)

        # Applying CLAHE to L-channel
        # feel free to try different values for the limit and grid size:
        clahe = cv2.createCLAHE(clipLimit=2.0, tileGridSize=(8,8))
        cl = clahe.apply(l_channel)

        # merge the CLAHE enhanced L-channel with the a and b channel
        limg = cv2.merge((cl,a,b))

        # Converting image from LAB Color model to BGR color spcae
        enhanced_img = cv2.cvtColor(limg, cv2.COLOR_LAB2BGR)
        process2 = cv2.cvtColor(enhanced_img, cv2.COLOR_BGR2GRAY)

        #sharpen_filter = np.array([[0, -1, 0], [-1, 5, -1], [0, -1, 0]])
        #kernel = np.array([[-1,-1,-1], [-1,9,-1], [-1,-1,-1]])
        #sharped_img = cv2.filter2D(resize, -1, kernel)
        #print(type(resize))
        sharpened_img = cv2.bilateralFilter(process2,9,75,75)
        thresh, im_bw = cv2.threshold(sharpened_img, 130, 255, cv2.THRESH_BINARY )
        eroded = thin_font(im_bw)
        cv2.imwrite(("output__"+sys.argv[6]+"__sharpened__"+sys.argv[5]+".png"), sharpened_img)
        cv2.imwrite(("output__"+sys.argv[6]+"__binaryfilter__"+sys.argv[5]+".png"), im_bw)
        cv2.imwrite(("output__"+sys.argv[6]+"__eroded__"+sys.argv[5]+".png"),eroded)
        data = ""
        if general2 == "true" or not melee=="True" or not melee=="False": #or melee=="False"
            data = pytesseract.image_to_string(eroded, config="--psm 6")
        else:
            data = pytesseract.image_to_string(im_bw, config="--psm 6")


        return data

print("hi im python\n")
print(sys.argv[0] + "\n" + sys.argv[1] + "\n" + sys.argv[2] + "\n" + sys.argv[3] + "\n" + sys.argv[4] + "\n" + sys.argv[5] + "\n" + sys.argv[6])
parser = ImageParser()
parser.parse_screenshot(sys.argv[1], sys.argv[3], sys.argv[4])