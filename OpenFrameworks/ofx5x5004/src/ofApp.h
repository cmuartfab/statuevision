#ifndef _TEST_APP
#define _TEST_APP

#include "ofMain.h"
#include "ofxAssimpModelLoader.h"
#include "ofVboMesh.h"
#include "ofxGui.h"
#include "ofxOsc.h"

// listen on port 12345
#define PORT_RECEIVE 12345
#define NUM_MSG_STRINGS 20

// sending on port 10000
#define PORT_SEND 10000
#define HOST "localhost"

class ofApp : public ofBaseApp{

	public:
		void setup();
		void update();
		void draw();
		
        //keyboard and mouse related
        //
		void keyPressed(int key);
		void keyReleased(int key);
		void mouseMoved(int x, int y );
		void mouseDragged(int x, int y, int button);
		void mousePressed(int x, int y, int button);
		void mouseReleased(int x, int y, int button);
		void windowResized(int w, int h);
		void dragEvent(ofDragInfo dragInfo);
		void gotMessage(ofMessage msg);		
    
        //animation related
        //
		bool bAnimate;
        bool bAnimateMouse;
        float animationPosition;
    
        //osc related
    
        //GUI related
        //
        ofxPanel gui;
        ofParameter<float> bendFreq;
        float bendFreqInit = 2.;
        float bendFreqMin = 0.;
        float bendFreqMax = 30.;
        ofParameter<float> bendAmount;
        float bendAmountInit = 0.;
        float bendAmountMin = -.03;
        float bendAmountMax = .03;
        ofParameter<float> bendAngle;
        float bendAngleInit = 10.;
        float bendAngleMin = 0.;
        float bendAngleMax = 90.;
        ofParameter<float> twistFreq;
        float twistFreqInit = 2.;
        float twistFreqMin = 0.;
        float twistFreqMax = 30.;
        ofParameter<float> twistAmount;
        float twistAmountInit = 0;
        float twistAmountMin = 0;
        float twistAmountMax = 15;
        ofParameter<float> twistHeight;
        float twistHeightInit = 0.1;
        float twistHeightMin = 0.0000000001;
        float twistHeightMax = .1;
        ofParameter<float> rotZ;
        float rotZInit = 0;
        float rotZMin = -180;
        float rotZMax = 180;
        ofParameter<float> modelX;
        float modelXInit;
        float modelXMin;
        float modelXMax;
        ofParameter<float> modelY;
        float modelYInit;
        float modelYMin;
        float modelYMax;
        ofParameter<float> modelZ;
        float modelZInit = 0;
        float modelZMin = -200.;
        float modelZMax = 200.;
        float modelXYLast[6];
        ofParameter<float> scale;
        float scaleInit = .5;
        float scaleMin = 0.;
        float scaleMax = 3.;
        ofParameter<float> exposure;
        float exposureInit = 1.;
        float exposureMin = 0.;
        float exposureMax = 10.;
        ofParameter<float> bright;
        float brightInit = 1.;
        float brightMin = 0.;
        float brightMax = 10.;
        //ofParameter<float> guiShow;
        int guiShow = 1;
        int animateShow = 0;
    
        ofParameter<float> textX;
        ofParameter<float> textY;
    
    
        //text
        ofTrueTypeFont lttf;
        ofTrueTypeFont font;
    
        //3d model related
        //
        ofxAssimpModelLoader model;
        vector<ofxAssimpModelLoader> models;
        ofDirectory modelDir;
        int modelCount;
        int modelCurrent;
        string modelCurrentName;
        string modelCurrentFile;
        string modelPath;
    
        //lighting related
        //
        ofMesh mesh;
        ofLight	light;
    
        //shader lrealted
        ofShader shader;
        bool doShader;
    
        //OSC related
        //void gotMessage(ofMessage msg);
    
        ofxOscReceiver receiver;
        ofxOscSender sender;
    
        int current_msg_string;
        string msg_strings[NUM_MSG_STRINGS];
        float timers[NUM_MSG_STRINGS];

};

#endif
